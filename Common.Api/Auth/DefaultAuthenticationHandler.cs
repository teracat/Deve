using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Deve.Auth;
using Deve.Localize;

namespace Deve.Api
{
    /// <summary>
    /// https://dotnetcorecentral.com/blog/authentication-handler-in-asp-net-core/
    /// </summary>
    public class DefaultAuthenticationOptions : AuthenticationSchemeOptions
    {
    }

    public class DefaultAuthenticationHandler : AuthenticationHandler<DefaultAuthenticationOptions>
    {
        #region Constructor
        public DefaultAuthenticationHandler(IOptionsMonitor<DefaultAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder)
            : base(options, logger, encoder)
        {
        }
        #endregion

        #region AuthenticationHandler
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.Run(() =>
            {
                try
                {
                    string? langCode = UtilsApi.GetLangCodeFromRequest(Request);
                    var options = new DataOptions();
                    if (!string.IsNullOrWhiteSpace(langCode))
                        options.LangCode = langCode;

                    if (!Request.Headers.ContainsKey("Authorization"))
                        return GetResultUnauthorized(options.LangCode);

                    string? authorizationHeader = Request.Headers["Authorization"];
                    if (string.IsNullOrEmpty(authorizationHeader))
                        return AuthenticateResult.NoResult();

                    if (!authorizationHeader.StartsWith(ApiConstants.ApiAuthDefaultScheme, StringComparison.OrdinalIgnoreCase))
                        return GetResultUnauthorized(options.LangCode);

                    string token = authorizationHeader.Substring(ApiConstants.ApiAuthDefaultScheme.Length).Trim();
                    if (string.IsNullOrEmpty(token))
                        return GetResultUnauthorized(options.LangCode);

                    return ValidateToken(token, options);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    return AuthenticateResult.Fail(ex.Message);
                }
            });
        }
        #endregion

        #region Methods
        private AuthenticateResult ValidateToken(string token, DataOptions options)
        {
            var auth = AuthFactory.Get(null, options);
            var res = auth.TokenManager.ValidateToken(token, out var tokenData);
            if (res == TokenParseResult.Valid && tokenData is not null)
            {
                var principal = UserConverter.ToClaimsPrincipal(Scheme.Name, tokenData);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            return GetResultUnauthorized(options.LangCode);
        }

        private AuthenticateResult GetResultUnauthorized(string langCode)
        {
            return AuthenticateResult.Fail(ErrorLocalizeFactory.Get().Localize(ResultErrorType.Unauthorized, langCode));
        }
        #endregion
    }
}
