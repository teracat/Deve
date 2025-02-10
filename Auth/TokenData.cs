using Deve.Internal.Model;

namespace Deve.Auth
{
    /// <summary>
    /// Information that includes the token (used in TokenManagerCrypt).
    /// </summary>
    public class TokenData
    {
        /// <summary>
        /// When the token expires.
        /// </summary>
        public DateTimeOffset Expires { get; set; }

        /// <summary>
        /// Information about the user associated to the token.
        /// </summary>
        public UserIdentity Subject { get; set; }

        /// <summary>
        /// Default constructor (needed for deserialization).
        /// </summary>
        public TokenData()
        {
            Subject = new UserIdentity();
            Expires = DateTimeOffset.UtcNow.AddHours(AuthConstants.TokenExpiresInHours);
        }

        /// <summary>
        /// Create a new TokenData from other token data.
        /// </summary>
        /// <param name="other">The other token to copy data from.</param>
        /// <param name="expires">When the token will expire.</param>
        public TokenData(TokenData other, DateTimeOffset expires)
        {
            Subject = other.Subject;
            Expires = other.Expires;
        }

        /// <summary>
        /// Create a new TokenData from the user and expires provided.
        /// </summary>
        /// <param name="user">The token will contain information from this user.</param>
        /// <param name="expires">When the token will expire.</param>
        public TokenData(User user, DateTimeOffset expires)
        {
            Subject = new UserIdentity(user);
            Expires = expires;
        }
    }
}