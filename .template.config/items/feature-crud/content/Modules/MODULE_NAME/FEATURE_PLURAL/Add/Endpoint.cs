namespace Deve.MODULE_NAME.FEATURE_PLURAL.Add;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPost(MODULE_NAMEConstants.PathFEATURE_SINGULARV1, async (FEATURE_SINGULARAddRequest request, IFEATURE_SINGULARData data, CancellationToken cancellationToken) =>
            await data.AddAsync(request, cancellationToken))
        .WithTags(MODULE_NAMEConstants.TagFEATURE_SINGULAR);
}
