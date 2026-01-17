namespace Deve.Auth.RefreshToken;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPost(ApiConstants.PathAuthV1 + ApiConstants.MethodRefreshToken, async (RefreshTokenRequest request, IAuthData data, CancellationToken cancellationToken) =>
            await data.RefreshToken(request, cancellationToken))
        .WithTags(ApiConstants.TagAuth);
}
