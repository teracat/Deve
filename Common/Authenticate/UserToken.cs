namespace Deve
{
    /// <summary>
    /// Class used as the login response with token information to authorize other requests.
    /// </summary>
    public class UserToken
    {
        #region Properties
        /// <summary>
        /// When the token has been created.
        /// </summary>
        public DateTimeOffset Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// When the token will expire.
        /// </summary>
        public DateTimeOffset Expires { get; set; }

        /// <summary>
        /// The token to be used to authorize other requests.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Which scheme to be used to validate the token.
        /// </summary>
        public string Scheme { get; set; }

        /// <summary>
        /// Public user information.
        /// </summary>
        public UserSubject Subject { get; set; }
        #endregion

        #region Constructors
        public UserToken()
        {
            Token = string.Empty;
            Scheme = ApiConstants.AuthDefaultScheme;
            Subject = new UserSubject();
        }

        public UserToken(UserToken other)
        {
            Expires = other.Expires;
            Token = other.Token;
            Scheme = other.Scheme;
            Subject = new UserSubject(other.Subject);
        }

        public UserToken(UserSubject subject, DateTimeOffset expires, string token, string scheme = ApiConstants.AuthDefaultScheme)
        {
            Subject = subject;
            Expires = expires;
            Token = token;
            Scheme = scheme;
        }
        #endregion
    }
}
