using System.Security.Claims;
using System.Security.Principal;
using Deve.Authenticate;
using Deve.Internal.Dto;

namespace Deve.Auth.Converters
{
    /// <summary>
    /// Provides utility methods for converting between <see cref="UserIdentity"/> and authentication-related objects.
    /// </summary>
    public static class UserConverter
    {
        /// <summary>
        /// Converts a <see cref="UserIdentity"/> into a <see cref="ClaimsIdentity"/> for authentication.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <param name="userIdentity">The user identity to convert.</param>
        /// <returns>A <see cref="ClaimsIdentity"/> containing user claims.</returns>
        public static ClaimsIdentity ToClaimsIdentity(string scheme, UserIdentity userIdentity)
        {
            List<Claim> claims =
            [
                new Claim(AuthConstants.UserClaimRole, RoleConverter.ToString(userIdentity.Role)),
                new Claim(AuthConstants.UserClaimId, userIdentity.Id.ToString()),
                new Claim(AuthConstants.UserClaimUsername, userIdentity.Username.ToString()),
            ];
            return new ClaimsIdentity(claims, scheme, AuthConstants.UserClaimUsername, AuthConstants.UserClaimRole);
        }

        /// <summary>
        /// Converts a <see cref="UserIdentity"/> into a <see cref="ClaimsPrincipal"/> for authentication.
        /// </summary>
        /// <param name="scheme">The authentication scheme.</param>
        /// <param name="userIdentity">The user identity to convert.</param>
        /// <returns>A <see cref="ClaimsPrincipal"/> containing user claims.</returns>
        public static ClaimsPrincipal ToClaimsPrincipal(string scheme, UserIdentity userIdentity)
        {
            var identity = ToClaimsIdentity(scheme, userIdentity);
            return new GenericPrincipal(identity, null);
        }

        /// <summary>
        /// Converts a <see cref="ClaimsPrincipal"/> into a <see cref="UserIdentity"/>.
        /// </summary>
        /// <param name="identity">The claims principal containing user information.</param>
        /// <returns>
        /// A <see cref="UserIdentity"/> populated with the extracted claims, or <c>null</c> if the claims are invalid.
        /// </returns>
        public static UserIdentity? ToUserIdentity(ClaimsPrincipal identity)
        {
            var idStr = GetClaimValue(identity, AuthConstants.UserClaimId);
            var username = GetClaimValue(identity, AuthConstants.UserClaimUsername);
            var role = GetClaimValue(identity, AuthConstants.UserClaimRole);
            if (Utils.SomeIsNullOrWhiteSpace(idStr, username, role) || !long.TryParse(idStr, out long id))
            {
                return null;
            }

            return new UserIdentity
            {
                Id = id,
                Username = username,
                Role = RoleConverter.ToRole(role)
            };
        }

        /// <summary>
        /// Retrieves the value of a specific claim from a <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="identity">The claims principal containing the claim.</param>
        /// <param name="claimName">The name of the claim to retrieve.</param>
        /// <returns>The claim value, or an empty string if not found.</returns>
        private static string GetClaimValue(ClaimsPrincipal identity, string claimName)
        {
            return identity.Claims
                           .FirstOrDefault(x => x.Type == claimName)?
                           .Value ?? string.Empty;
        }

        /// <summary>
        /// Converts a <see cref="User"/> into a <see cref="UserSubject"/> containing limited user details.
        /// </summary>
        /// <param name="user">The user entity to convert.</param>
        /// <returns>A <see cref="UserSubject"/> containing selected user details.</returns>
        public static UserSubject ToUserSubject(User user)
        {
            return new UserSubject()
            {
                Name = user.Name,
                Username = user.Username,
                Joined = user.Joined,
            };
        }
    }
}
