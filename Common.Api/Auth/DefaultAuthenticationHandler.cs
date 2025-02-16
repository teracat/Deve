using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Deve.Localize;
using Deve.Data;
using Deve.Logging;
using Deve.Model;
using Deve.Auth.TokenManagers;
using Deve.Auth.Converters;

namespace Deve.Api.Auth
{
    /// <summary>
    /// https://dotnetcorecentral.com/blog/authentication-handler-in-asp-net-core/
    /// </summary>
    public class DefaultAuthenticationOptions : AuthenticationSchemeOptions
    {
    }

    public class DefaultAuthenticationHandler : AuthenticationHandler<DefaultAuthenticationOptions>
    {
        #region Fields
        private readonly ITokenManager _tokenManager;
        #endregion

        #region Constructor
        public DefaultAuthenticationHandler(IOptionsMonitor<DefaultAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ITokenManager tokenManager)
            : base(options, logger, encoder)
        {
            _tokenManager = tokenManager;
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
                    {
                        options.LangCode = langCode;
                    }

                    if (!Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues authorizationHeader))
                    {
                        return GetResultUnauthorized(options.LangCode);
                    }

                    string? authHeaderString = authorizationHeader;
                    if (string.IsNullOrWhiteSpace(authHeaderString))
                    {
                        return GetResultUnauthorized(options.LangCode);
                    }

                    var parts = authHeaderString.Split(' ');
                    if (parts.Length != 2)
                    {
                        return GetResultUnauthorized(options.LangCode);
                    }

                    string scheme = parts[0] ?? string.Empty;
                    string token = parts[1] ?? string.Empty;

                    if (Utils.SomeIsNullOrWhiteSpace(scheme, token))
                    {
                        return GetResultUnauthorized(options.LangCode);
                    }

                    return ValidateToken(scheme, token, options);
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
        private AuthenticateResult ValidateToken(string scheme, string token, DataOptions options)
        {
            if (_tokenManager.TryValidateToken(token, out var userIdentity) && userIdentity is not null)
            {
                var principal = UserConverter.ToClaimsPrincipal(scheme, userIdentity);
                var ticket = new AuthenticationTicket(principal, scheme);
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
