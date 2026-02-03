namespace Deve.MODULE_NAME.FEATURE_PLURAL.METHOD_NAME;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPatch(MODULE_NAMEConstants.PathFEATURE_SINGULARV1 + "{id:guid}/" + MODULE_NAMEConstants.MethodMETHOD_NAME, async (Guid id, FEATURE_SINGULARMETHOD_NAMERequest request, IFEATURE_SINGULARData data, CancellationToken cancellationToken) =>
            await data.METHOD_NAMEAsync(id, request, cancellationToken))
        .WithTags(MODULE_NAMEConstants.TagFEATURE_SINGULAR);
}
