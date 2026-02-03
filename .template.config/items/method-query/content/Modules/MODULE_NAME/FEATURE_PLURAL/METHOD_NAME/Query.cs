namespace Deve.MODULE_NAME.FEATURE_PLURAL.METHOD_NAME;

internal sealed record Query : IRequest<ResultGet<FEATURE_SINGULARMETHOD_NAMEResponse>>
{
    public required Guid Id { get; init; }
}
