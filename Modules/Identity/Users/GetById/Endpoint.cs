namespace Deve.Identity.Users.GetById;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(IdentityConstants.PathUserV1 + "{id:guid}", async (Guid id, IUserData data, CancellationToken cancellationToken) =>
            await data.GetByIdAsync(id, cancellationToken))
        .WithTags(IdentityConstants.TagUser);
}
