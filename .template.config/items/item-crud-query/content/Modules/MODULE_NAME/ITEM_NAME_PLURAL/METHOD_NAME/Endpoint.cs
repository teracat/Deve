namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAME;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(MODULE_NAMEConstants.PathITEM_NAME_SINGULARV1 + SalesConstants.MethodMETHOD_NAME, async ([AsParameters] ITEM_NAME_SINGULARMETHOD_NAMERequest request, IITEM_NAME_SINGULARData data, CancellationToken cancellationToken) =>
            await data.METHOD_NAMEAsync(request, cancellationToken))
        .WithTags(MODULE_NAMEConstants.TagITEM_NAME_SINGULAR);
}
