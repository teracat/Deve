namespace Deve.MODULE_NAME.FEATURE_PLURAL.Add;

internal sealed record Command : IRequest<ResultGet<ResponseId>>
{
    public required string Name { get; init; }
}
