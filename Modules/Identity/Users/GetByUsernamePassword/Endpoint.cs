namespace Deve.Identity.Users.GetByUsernamePassword;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(ApiConstants.PathUserV1 + ApiConstants.MethodGetByUsernamePassword, async ([AsParameters] UserGetByUsernamePasswordRequest request, IUserData data, CancellationToken cancellationToken) =>
            await data.GetByUsernamePasswordAsync(request, cancellationToken))
        .WithTags(ApiConstants.TagIdentityUser);
}
