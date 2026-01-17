namespace Deve.Identity.Users.UpdatePassword;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPatch(ApiConstants.PathUserV1 + "{id:guid}/" + ApiConstants.MethodPassword, async (Guid id, UserUpdatePasswordRequest request, IUserData data, CancellationToken cancellationToken) =>
            await data.UpdatePasswordAsync(id, request, cancellationToken))
        .WithTags(ApiConstants.TagIdentityUser);
}
