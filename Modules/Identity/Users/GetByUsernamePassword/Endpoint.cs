namespace Deve.Identity.Users.GetByUsernamePassword;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(IdentityConstants.PathUserV1 + IdentityConstants.MethodGetByUsernamePassword, async ([AsParameters] UserGetByUsernamePasswordRequest request, IUserData data, CancellationToken cancellationToken) =>
            await data.GetByUsernamePasswordAsync(request, cancellationToken))
        .WithTags(IdentityConstants.TagUser);
}
