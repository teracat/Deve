namespace Deve.MODULE_NAME.FEATURE_PLURAL.GetList;

internal sealed class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder builder) =>
        _ = builder.MapGet(MODULE_NAMEConstants.PathFEATURE_SINGULARV1, async ([AsParameters] FEATURE_SINGULARGetListRequest request, IFEATURE_SINGULARData data, CancellationToken cancellationToken) =>
            await data.GetAsync(request, cancellationToken))
        .WithTags(MODULE_NAMEConstants.TagFEATURE_SINGULAR);
}
