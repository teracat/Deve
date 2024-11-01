using System.Security.Principal;
using System.Security.Claims;

namespace Deve.Auth
{
    public static class UserConverter
    {
        public static ClaimsPrincipal ToClaimsPrincipal(string scheme, TokenData tokenData)
        {
            List<Claim> claims = [
                new Claim(AuthConstants.UserClaimRole, RoleConverter.ToString(tokenData.Subject.Role)),
                new Claim(AuthConstants.UserClaimId, tokenData.Subject.Id.ToString())
            ];
            var identity = new ClaimsIdentity(claims, scheme);
            return new GenericPrincipal(identity, null);
        }

        public static UserSubject ToUserSubject(User user)
        {
            return new UserSubject()
            {
                Name = user.Name,
                Username = user.Username,
                Joined = user.Joined,
                Role = user.Role,
            };
        }

        public static UserIdentity? ToUserIdentity(ClaimsPrincipal identity)
        {
            var idStr = GetClaimValue(identity, AuthConstants.UserClaimId);
            var role = GetClaimValue(identity, AuthConstants.UserClaimRole);
            if (Utils.SomeIsNullOrWhiteSpace(idStr, role) || !long.TryParse(idStr, out long id))
                return null;

            return new UserIdentity()
            {
                Id = id,
                Role = RoleConverter.ToRole(role)
            };
        }

        private static string? GetClaimValue(ClaimsPrincipal identity, string claimName)
        {
            return identity.Claims
                           .FirstOrDefault(x => x.Type == claimName)?
                           .Value;
        }
    }
}
