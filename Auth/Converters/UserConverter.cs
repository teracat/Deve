using System.Security.Principal;
using System.Security.Claims;
using Deve.Authenticate;
using Deve.Internal.Model;

namespace Deve.Auth.Converters
{
    public static class UserConverter
    {
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

        public static ClaimsPrincipal ToClaimsPrincipal(string scheme, UserIdentity userIdentity)
        {
            var identity = ToClaimsIdentity(scheme, userIdentity);
            return new GenericPrincipal(identity, null);
        }

        public static UserIdentity? ToUserIdentity(ClaimsPrincipal identity)
        {
            var idStr = GetClaimValue(identity, AuthConstants.UserClaimId);
            var username = GetClaimValue(identity, AuthConstants.UserClaimUsername);
            var role = GetClaimValue(identity, AuthConstants.UserClaimRole);
            if (Utils.SomeIsNullOrWhiteSpace(idStr, username, role) || !long.TryParse(idStr, out long id))
            {
                return null;
            }

            return new UserIdentity()
            {
                Id = id,
                Username = username,
                Role = RoleConverter.ToRole(role)
            };
        }

        private static string GetClaimValue(ClaimsPrincipal identity, string claimName)
        {
            return identity.Claims
                           .FirstOrDefault(x => x.Type == claimName)?
                           .Value ?? string.Empty;
        }

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
