namespace Deve.Auth.Login;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPost(ApiConstants.PathAuthV1 + ApiConstants.MethodLogin, async (LoginRequest request, IAuthData data, CancellationToken cancellationToken) =>
            await data.Login(request, cancellationToken))
        .WithTags(ApiConstants.TagAuth);
}
