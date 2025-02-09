using Deve.Authenticate;
using Deve.Model;

namespace Deve.Core
{
    internal class CoreAuth : CoreBase, IAuthenticate
    {
        #region Constructor
        public CoreAuth(CoreMain core)
            : base(core)
        {
        }
        #endregion

        #region IAuthenticate
        public async Task<ResultGet<UserToken>> Login(UserCredentials userCredentials)
        {
            //Uses specific Config defined in ShieldConfig
            var resShield = await Core.Shield.Protect(Core.Options);
            if (!resShield.Success)
                return Utils.ResultGetError<UserToken>(resShield);

            var res = await Core.Auth.LoginUser(userCredentials);

            await Core.Shield.SetAttemptResult(res.Success, Core.Options);

            if (!res.Success)
                return Utils.ResultGetError<UserToken>(res);
            
            if (res.Data is null)
                return Utils.ResultGetError<UserToken>(Core.Options.LangCode, ResultErrorType.Unauthorized);

            if (res.Success && Core.IsSharedInstance)
                Core.User = res.Data;

            UserToken userToken = Core.Auth.TokenManager.CreateToken(res.Data);
            return Utils.ResultGetOk(userToken);
        }

        public async Task<ResultGet<UserToken>> RefreshToken(string token)
        {
            //Uses default Config defined in ShieldItemConfig
            var resShield = await Core.Shield.Protect(Core.Options);
            if (!resShield.Success)
                return Utils.ResultGetError<UserToken>(resShield);

            var resToken = await Core.Auth.RefreshToken(token);

            await Core.Shield.SetAttemptResult(resToken.Success, Core.Options);

            return resToken;
        }
        #endregion
    }
}