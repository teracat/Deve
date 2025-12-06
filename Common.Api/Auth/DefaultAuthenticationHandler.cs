using System.Text.Encodings.Web;
using Deve.Auth.Converters;
using Deve.Auth.TokenManagers;
using Deve.Data;
using Deve.Localize;
using Deve.Logging;
using Deve.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Deve.Api.Auth
{
    /// <summary>
    /// Represents authentication scheme options for the default authentication handler.
    /// https://dotnetcorecentral.com/blog/authentication-handler-in-asp-net-core/
    /// </summary>
    public class DefaultAuthenticationOptions : AuthenticationSchemeOptions
    {
    }

    /// <summary>
    /// Handles authentication by validating tokens provided in the Authorization header.
    /// </summary>
    public class DefaultAuthenticationHandler : AuthenticationHandler<DefaultAuthenticationOptions>
    {
        #region Fields
        // <summary>
        /// Manages token validation and authentication.
        /// </summary>
        private readonly ITokenManager _tokenManager;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="options">The authentication scheme options.</param>
        /// <param name="logger">The logger factory used for logging.</param>
        /// <param name="encoder">The URL encoder.</param>
        /// <param name="tokenManager">The token manager used for token validation.</param>
        public DefaultAuthenticationHandler(IOptionsMonitor<DefaultAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ITokenManager tokenManager)
            : base(options, logger, encoder)
        {
            _tokenManager = tokenManager;
        }
        #endregion

        #region AuthenticationHandler
        /// <summary>
        /// Handles authentication by extracting and validating the token from the Authorization header.
        /// </summary>
        /// <returns>
        /// An authentication result indicating whether authentication was successful or failed.
        /// </returns>
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
        /// <summary>
        /// Validates the provided token using the token manager.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <param name="token">The token to validate.</param>
        /// <param name="options">Additional options such as the language code.</param>
        /// <returns>
        /// A successful authentication result if the token is valid; otherwise, an unauthorized result.
        /// </returns>
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

        /// <summary>
        /// Returns an unauthorized authentication result with a localized error message.
        /// </summary>
        /// <param name="langCode">The language code for localization.</param>
        /// <returns>An authentication failure result.</returns>
        private AuthenticateResult GetResultUnauthorized(string langCode) => AuthenticateResult.Fail(ErrorLocalizeFactory.Get().Localize(ResultErrorType.Unauthorized, langCode));
        #endregion
    }
}
