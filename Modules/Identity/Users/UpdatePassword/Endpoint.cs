namespace Deve.Identity.Users.UpdatePassword;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPatch(IdentityConstants.PathUserV1 + "{id:guid}/" + IdentityConstants.MethodPassword, async (Guid id, UserUpdatePasswordRequest request, IUserData data, CancellationToken cancellationToken) =>
            await data.UpdatePasswordAsync(id, request, cancellationToken))
        .WithTags(IdentityConstants.TagIdentityUser);
}
