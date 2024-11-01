using System.Net.Http.Json;

namespace Deve.Sdk
{
    internal class SdkAuth : SdkBase<ISdkCommon>, IAuthenticate
    {
        #region Properties
        protected override string Path => ApiConstants.ApiPathAuth;
        #endregion

        #region Constructor
        public SdkAuth(ISdkCommon sdk)
            : base(sdk)
        {
        }
        #endregion

        #region IAuthenticate
        public async Task<ResultGet<UserToken>> Login(UserCredentials credentials)
        {
            try
            {
                var queryBuidler = new UriQuery();
                queryBuidler.AddParameter("username", credentials.Username);
                queryBuidler.AddParameter("password", credentials.Password);
                var res = await Sdk.Client.GetFromJsonAsync<ResultGet<UserToken>>(Path + ApiConstants.ApiMethodLogin + queryBuidler.ToQueryString(), SerializerOptions);
                if (res is null)
                    return Utils.ResultGetError<UserToken>(Sdk.Options.LangCode, ResultErrorType.Unknown);

                Sdk.UserToken = res.Data;
                return res;
            }
            catch (Exception ex)
            {
                return Utils.ResultGetError<UserToken>(ResultErrorType.Unknown, null, ex.Message);
            }
        }

        public async Task<ResultGet<UserToken>> RefreshToken(string token)
        {
            try
            {
                var queryBuidler = new UriQuery();
                queryBuidler.AddParameter("token", token);
                var res = await Sdk.Client.GetFromJsonAsync<ResultGet<UserToken>>(Path + ApiConstants.ApiMethodRefreshToken + queryBuidler.ToQueryString(), SerializerOptions);
                if (res is null)
                    return Utils.ResultGetError<UserToken>(Sdk.Options.LangCode, ResultErrorType.Unknown);
                return res;
            }
            catch (Exception ex)
            {
                return Utils.ResultGetError<UserToken>(ResultErrorType.Unknown, null, ex.Message);
            }
        }
        #endregion
    }
}
