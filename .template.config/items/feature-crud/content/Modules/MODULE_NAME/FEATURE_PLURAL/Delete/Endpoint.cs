namespace Deve.MODULE_NAME.FEATURE_PLURAL.Delete;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapDelete(MODULE_NAMEConstants.PathFEATURE_SINGULARV1 + "{id:guid}", async (Guid id, IFEATURE_SINGULARData data, CancellationToken cancellationToken) =>
            await data.DeleteAsync(id, cancellationToken))
        .WithTags(MODULE_NAMEConstants.TagFEATURE_SINGULAR);
}
