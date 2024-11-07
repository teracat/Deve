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
            var res = await Core.Auth.LoginUser(userCredentials);
            if (!res.Success)
                return Utils.ResultGetError<UserToken>(res);
            
            if (res.Data is null)
                return Utils.ResultGetError<UserToken>(Core.Options.LangCode, ResultErrorType.Unauthorized);
            
            if (res.Success && Core.IsSharedInstance)
                Core.User = res.Data.User;
            
            return Utils.ResultGetOk(res.Data.UserToken);
        }

        public Task<ResultGet<UserToken>> RefreshToken(string token)
        {
            return Core.Auth.RefreshToken(token);
        }
        #endregion
    }
}