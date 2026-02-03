namespace Deve.MODULE_NAME.FEATURE_PLURAL.Update;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPut(MODULE_NAMEConstants.PathFEATURE_SINGULARV1 + "{id:guid}", async (Guid id, FEATURE_SINGULARUpdateRequest request, IFEATURE_SINGULARData data, CancellationToken cancellationToken) =>
            await data.UpdateAsync(id, request, cancellationToken))
        .WithTags(MODULE_NAMEConstants.TagFEATURE_SINGULAR);
}
