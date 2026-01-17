namespace Deve.Identity.Users.Update;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPut(ApiConstants.PathUserV1 + "{id:guid}", async (Guid id, UserUpdateRequest request, IUserData data, CancellationToken cancellationToken) =>
            await data.UpdateAsync(id, request, cancellationToken))
        .WithTags(ApiConstants.TagIdentityUser);
}
