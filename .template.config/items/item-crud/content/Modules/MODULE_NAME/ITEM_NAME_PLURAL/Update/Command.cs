namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.Update;

internal sealed record Command(Guid Id, string Name) : IRequest<Result>;
