using Deve.DataSource;
using Deve.Data;
using Deve.Model;
using Deve.Authenticate;
using Deve.Criteria;
using Deve.Auth.TokenManagers;
using Deve.Auth.Hash;
using Deve.Auth.Permissions;
using Deve.Internal.Model;
using Deve.Internal.Criteria;

namespace Deve.Auth
{
    /// <summary>
    /// Auth implementation.
    /// </summary>
    public class AuthMain : IAuth
    {
        #region Properties
        public IDataOptions Options { get; set; }
        public IDataSource DataSource { get; }
        public ITokenManager TokenManager { get; }
        public IHash Hash { get; }
        #endregion

        #region Constructor
        public AuthMain(ITokenManager tokenManager, IDataSource dataSource, IHash hash, IDataOptions options)
        {
            TokenManager = tokenManager;
            DataSource = dataSource;
            Hash = hash;
            Options = options;
        }
        #endregion

        #region Methods
        private PermissionResult HasUser(IUserIdentity? user)
        {
            if (user is null)
            {
                return PermissionResult.Unauthorized;
            }
            return PermissionResult.Granted;
        }

        private PermissionResult HasUserAdmin(IUserIdentity? user)
        {
            if (user is null)
            {
                return PermissionResult.Unauthorized;
            }

            if (user.Role != Role.Admin)
            {
                return PermissionResult.NotGranted;
            }

            return PermissionResult.Granted;
        }
        #endregion

        #region IAuth
        public async Task<ResultGet<User>> LoginUser(UserCredentials userCredentials)
        {
            if (userCredentials is null || Utils.SomeIsNullOrWhiteSpace(userCredentials.Username, userCredentials.Password))
            {
                return Utils.ResultGetError<User>(Options.LangCode, ResultErrorType.Unauthorized);
            }

            var passwordHash = Hash.Calc(userCredentials.Password);
            var resUsers = await DataSource.Users.Get(new CriteriaUser()
            {
                Username = userCredentials.Username,
                PasswordHash = passwordHash,
                OnlyActive = CriteriaActiveType.OnlyActive,
            });
            if (!resUsers.Success)
            {
                return Utils.ResultGetError<User>(resUsers);
            }

            var user = resUsers.Data.FirstOrDefault();
            if (user is null)
            {
                return Utils.ResultGetError<User>(Options.LangCode, ResultErrorType.Unauthorized);
            }

            return Utils.ResultGetOk(user);
        }

        public Task<PermissionResult> IsGranted(IUserIdentity? user, PermissionType type, PermissionDataType dataType)
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
                        {
                            return PermissionResult.Granted;
                        }

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
            {
                return Utils.ResultGetError<UserToken>(resLogin);
            }

            if (resLogin.Data is null)
            {
                return Utils.ResultGetError<UserToken>(Options.LangCode, ResultErrorType.Unauthorized);
            }

            var userToken = TokenManager.CreateToken(resLogin.Data);
            return Utils.ResultGetOk(userToken);
        }

        public Task<ResultGet<UserToken>> RefreshToken(string token)
        {
            return Task.Run(async () =>
            {
                if (!TokenManager.TryValidateToken(token, out var userIdentity) || userIdentity is null)
                {
                    return Utils.ResultGetError<UserToken>(Options.LangCode, ResultErrorType.Unauthorized);
                }

                var resUsers = await DataSource.Users.Get(userIdentity.Id);
                if (!resUsers.Success || resUsers.Data is null)
                {
                    return Utils.ResultGetError<UserToken>(resUsers);
                }

                if (!resUsers.Data.IsActive)
                {
                    return Utils.ResultGetError<UserToken>(Options.LangCode, ResultErrorType.Unauthorized);
                }

                var newUserToken = TokenManager.CreateToken(resUsers.Data);
                return Utils.ResultGetOk(newUserToken);
            });
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            // Nothing to dispose
        }
        #endregion
    }
}
