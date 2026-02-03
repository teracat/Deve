namespace Deve.MODULE_NAME.FEATURE_PLURAL.GetById;

internal sealed record Query(Guid Id) : IRequest<ResultGet<FEATURE_SINGULARResponse>>;
