using Microsoft.AspNetCore.Http;
using Deve.Auth.Mappers;
using Deve.Auth.UserIdentityService;
using Deve.Identity.Enums;
using Deve.Auth;

namespace Deve.Api.Auth;

/// <summary>
/// UserIdentity requesting the data, retrieved using the IHttpContextAccessor.
/// </summary>
public class HttpContextUserIdentityService : IUserIdentityService
{
    /// <summary>
    /// User identity.
    /// </summary>
    public UserIdentity? UserIdentity { get; set; }

    public bool IsAuthenticated => UserIdentity is not null;

    public bool IsAdmin => UserIdentity is not null && UserIdentity.Role == Role.Admin;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="contextAccessor">HttpContext accessor.</param>
    public HttpContextUserIdentityService(IHttpContextAccessor contextAccessor)
    {
        var claims = contextAccessor?.HttpContext?.User;
        if (claims?.Identity?.IsAuthenticated ?? false)
        {
            UserIdentity = UserIdentityMapper.ToUserIdentity(claims);
        }
    }
}
