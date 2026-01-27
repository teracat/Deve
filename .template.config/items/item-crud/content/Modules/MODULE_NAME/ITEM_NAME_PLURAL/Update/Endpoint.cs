namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.Update;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPut(MODULE_NAMEConstants.PathITEM_NAME_SINGULARV1 + "{id:guid}", async (Guid id, ITEM_NAME_SINGULARUpdateRequest request, IITEM_NAME_SINGULARData data, CancellationToken cancellationToken) =>
            await data.UpdateAsync(id, request, cancellationToken))
        .WithTags(MODULE_NAMEConstants.TagITEM_NAME_SINGULAR);
}
