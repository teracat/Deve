namespace Deve.Auth.RefreshToken;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPost(AuthConstants.PathAuthV1 + AuthConstants.MethodRefreshToken, async (RefreshTokenRequest request, IAuthData data, CancellationToken cancellationToken) =>
            await data.RefreshToken(request, cancellationToken))
        .WithTags(AuthConstants.TagAuth);
}
