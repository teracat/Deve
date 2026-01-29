namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.Delete;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapDelete(MODULE_NAMEConstants.PathITEM_NAME_SINGULARV1 + "{id:guid}", async (Guid id, IITEM_NAME_SINGULARData data, CancellationToken cancellationToken) =>
            await data.DeleteAsync(id, cancellationToken))
        .WithTags(MODULE_NAMEConstants.TagITEM_NAME_SINGULAR);
}
