namespace Deve.MODULE_NAME.FEATURE_PLURAL.GetById;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(MODULE_NAMEConstants.PathFEATURE_SINGULARV1 + "{id:guid}", async (Guid id, IFEATURE_SINGULARData data, CancellationToken cancellationToken) =>
            await data.GetByIdAsync(id, cancellationToken))
        .WithTags(MODULE_NAMEConstants.TagFEATURE_SINGULAR);
}
