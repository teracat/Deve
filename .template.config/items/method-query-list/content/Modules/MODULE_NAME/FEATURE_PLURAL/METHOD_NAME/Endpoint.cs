namespace Deve.MODULE_NAME.FEATURE_PLURAL.METHOD_NAME;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(MODULE_NAMEConstants.PathFEATURE_SINGULARV1 + SalesConstants.MethodMETHOD_NAME, async ([AsParameters] FEATURE_SINGULARMETHOD_NAMERequest request, IFEATURE_SINGULARData data, CancellationToken cancellationToken) =>
            await data.METHOD_NAMEAsync(request, cancellationToken))
        .WithTags(MODULE_NAMEConstants.TagFEATURE_SINGULAR);
}
