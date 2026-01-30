namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.GetList;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(MODULE_NAMEConstants.PathITEM_NAME_SINGULARV1, async ([AsParameters] ITEM_NAME_SINGULARGetListRequest request, IITEM_NAME_SINGULARData data, CancellationToken cancellationToken) =>
            await data.GetAsync(request, cancellationToken))
        .WithTags(MODULE_NAMEConstants.TagITEM_NAME_SINGULAR);
}
