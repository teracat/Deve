using Deve.Internal;
using Deve.DataSource;

namespace Deve.Auth
{
    internal class AuthMain : IAuth
    {
        #region Fields
        private DataOptions _options;
        #endregion

        #region Properties
        public DataOptions Options
        {
            get => _options;
            set => _options = value;
        }
        public IDataSource DataSource { get; }
        public ITokenManager TokenManager { get; }
        public IHash Hash { get; }
        public ICrypt Crypt { get; }
        #endregion

        #region Constructor
        public AuthMain(IDataSource? dataSource = null, DataOptions? options = null)
        {
            _options = options ?? new DataOptions();
            DataSource = dataSource ?? DataSourceFactory.Get(_options);
            Hash = new Hash();
            Crypt = new Crypt();
            TokenManager = new TokenManager(Crypt);
        }
        #endregion

        #region Methods
        private PermissionResult HasUser(UserIdentity? user)
        {
            if (user is null)
                return PermissionResult.Unauthorized;
            return PermissionResult.Granted;
        }

        private PermissionResult HasUserAdmin(UserIdentity? user)
        {
            if (user is null)
                return PermissionResult.Unauthorized;
            if (user.Role != Role.Admin)
                return PermissionResult.NotGranted;
            return PermissionResult.Granted;
        }
        #endregion

        #region IAuth
        public async Task<ResultGet<UserAndUserToken>> LoginUser(UserCredentials userCredentials)
        {
            if (userCredentials is null || Utils.SomeIsNullOrWhiteSpace(userCredentials.Username, userCredentials.Password))
                return Utils.ResultGetError<UserAndUserToken>(_options.LangCode, ResultErrorType.Unauthorized);

            var passwordHash = Hash.Calc(userCredentials.Password);
            var resUsers = await DataSource.Users.Get(new CriteriaUser()
            {
                Username = userCredentials.Username,
                PasswordHash = passwordHash,
                OnlyActive = CriteriaActiveType.OnlyActive,
            });
            if (!resUsers.Success)
                return Utils.ResultGetError<UserAndUserToken>(resUsers);

            var user = resUsers.Data.FirstOrDefault();
            if (user is null)
                return Utils.ResultGetError<UserAndUserToken>(_options.LangCode, ResultErrorType.Unauthorized);

            var userToken = TokenManager.CreateToken(user);

            return Utils.ResultGetOk(new UserAndUserToken(user, userToken));
        }

        public Task<PermissionResult> IsGranted(UserIdentity? user, PermissionType type, PermissionDataType dataType)
        {
            return Task.Run(() =>
            {
                switch (dataType)
                {
                    //Users, Clients & Stats -> Only Admins can perform all actions
                    case PermissionDataType.User:
                    case PermissionDataType.Client:
                    case PermissionDataType.Stats:
                        return HasUserAdmin(user);

                    case PermissionDataType.ClientBasic:
                        //Client Basic information can be accessed only by authenticated users
                        return HasUser(user);

                    case PermissionDataType.City:
                    case PermissionDataType.State:
                    case PermissionDataType.Country:
                        //Anyone can Get City, State & Country
                        if (type == PermissionType.Get || type == PermissionType.GetList)
                            return PermissionResult.Granted;

                        //Only Admins can Add, Update & Delete them
                        return HasUserAdmin(user);

                    default:
                        return PermissionResult.Unauthorized;
                }
            });
        }
        #endregion

        #region IAuthenticate
        public async Task<ResultGet<UserToken>> Login(UserCredentials userCredentials)
        {
            var resLogin = await LoginUser(userCredentials);
            if (!resLogin.Success)
                return Utils.ResultGetError<UserToken>(resLogin);
            if (resLogin.Data is null)
                return Utils.ResultGetError<UserToken>(_options.LangCode, ResultErrorType.Unauthorized);

            return Utils.ResultGetOk(resLogin.Data.UserToken);
        }

        public Task<ResultGet<UserToken>> RefreshToken(string token)
        {
            return Task.Run(async () =>
            {
                var validateRes = TokenManager.ValidateToken(token, out var tokenData);
                if (validateRes != TokenParseResult.Valid || tokenData is null)
                    return Utils.ResultGetError<UserToken>(_options.LangCode, ResultErrorType.Unauthorized);

                var resUsers = await DataSource.Users.Get(tokenData.Subject.Id);
                if (!resUsers.Success || resUsers.Data is null)
                    return Utils.ResultGetError<UserToken>(resUsers);

                if (!resUsers.Data.IsActive)
                    return Utils.ResultGetError<UserToken>(_options.LangCode, ResultErrorType.Unauthorized);

                var newTokenData = new TokenData(tokenData, DateTime.UtcNow.AddHours(AuthConstants.TokenExpiresInHours));
                var newUserToken = TokenManager.CreateToken(resUsers.Data);
                return Utils.ResultGetOk(newUserToken);
            });
        }
        #endregion
    }
}
