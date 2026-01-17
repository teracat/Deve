namespace Deve.Identity.Users.Delete;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapDelete(ApiConstants.PathUserV1 + "{id:guid}", async (Guid id, IUserData data, CancellationToken cancellationToken) =>
            await data.DeleteAsync(id, cancellationToken))
        .WithTags(ApiConstants.TagIdentityUser);
}
