namespace Deve.Identity.Users.GetList;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(IdentityConstants.PathUserV1, async ([AsParameters] UserGetListRequest request, IUserData data, CancellationToken cancellationToken) =>
            await data.GetAsync(request, cancellationToken))
        .WithTags(IdentityConstants.TagUser);
}
