using Deve.Auth;
using Deve.Authenticate;
using Deve.Core.Shield;
using Deve.Data;
using Deve.DataSource;
using Deve.Model;

namespace Deve.Core
{
    public class CoreAuth : CoreBase, IAuthenticate
    {
        #region Fields
        private readonly ICore? _core;
        private readonly IShield _shield;
        #endregion

        #region Constructor
        public CoreAuth(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentity? userIdentity, IShield shield)
            : base(dataSource, auth, options, userIdentity)
        {
            _core = null;
            _shield = shield;
        }

        public CoreAuth(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentity? userIdentity, ICore core)
            : base(dataSource, auth, options, userIdentity)
        {
            _core = core;
            _shield = core.Shield;
        }
        #endregion

        #region IAuthenticate
        public async Task<ResultGet<UserToken>> Login(UserCredentials userCredentials)
        {
            //Uses specific Config defined in ShieldConfig
            var resShield = await _shield.Protect(Options);
            if (!resShield.Success)
            {
                return Utils.ResultGetError<UserToken>(resShield);
            }

            var res = await Auth.LoginUser(userCredentials);

            await _shield.SetAttemptResult(res.Success, Options);

            if (!res.Success)
            {
                return Utils.ResultGetError<UserToken>(res);
            }

            if (res.Data is null)
            {
                return Utils.ResultGetError<UserToken>(Options.LangCode, ResultErrorType.Unauthorized);
            }

            // If the Login succeeded, we store the User in the Core instance (usefull for embedded clients)
            if (res.Success && _core is not null)
            {
                _core.User = res.Data;
            }

            UserToken userToken = Auth.TokenManager.CreateToken(res.Data);
            return Utils.ResultGetOk(userToken);
        }

        public async Task<ResultGet<UserToken>> RefreshToken(string token)
        {
            //Uses default Config defined in ShieldItemConfig
            var resShield = await _shield.Protect(Options);
            if (!resShield.Success)
            {
                return Utils.ResultGetError<UserToken>(resShield);
            }

            var resToken = await Auth.RefreshToken(token);

            await _shield.SetAttemptResult(resToken.Success, Options);

            return resToken;
        }
        #endregion
    }
}
