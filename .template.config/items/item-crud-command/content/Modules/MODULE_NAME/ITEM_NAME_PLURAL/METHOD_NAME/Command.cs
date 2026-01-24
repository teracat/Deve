namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAME;

internal sealed record Command(Guid Id, string Name) : IRequest<Result>;
