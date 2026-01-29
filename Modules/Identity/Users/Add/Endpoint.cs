namespace Deve.Identity.Users.Add;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPost(IdentityConstants.PathUserV1, async (UserAddRequest request, IUserData data, CancellationToken cancellationToken) =>
            await data.AddAsync(request, cancellationToken))
        .WithTags(IdentityConstants.TagUser);
}
