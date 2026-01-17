namespace Deve.Identity.Users.GetById;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(ApiConstants.PathUserV1 + "{id:guid}", async (Guid id, IUserData data, CancellationToken cancellationToken) =>
            await data.GetByIdAsync(id, cancellationToken))
        .WithTags(ApiConstants.TagIdentityUser);
}
