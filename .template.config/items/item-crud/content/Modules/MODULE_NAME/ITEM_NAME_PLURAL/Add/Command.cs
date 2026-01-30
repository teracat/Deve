namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.Add;

internal sealed record Command : IRequest<ResultGet<ResponseId>>
{
    public required string Name { get; init; }
}
