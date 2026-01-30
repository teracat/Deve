namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.Update;

internal sealed record Command : IRequest<Result>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}
