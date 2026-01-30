namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.Add;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapPost(MODULE_NAMEConstants.PathITEM_NAME_SINGULARV1, async (ITEM_NAME_SINGULARAddRequest request, IITEM_NAME_SINGULARData data, CancellationToken cancellationToken) =>
            await data.AddAsync(request, cancellationToken))
        .WithTags(MODULE_NAMEConstants.TagITEM_NAME_SINGULAR);
}
