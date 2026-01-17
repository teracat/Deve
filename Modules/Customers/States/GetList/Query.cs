namespace Deve.Customers.States.GetList;

internal sealed record Query(Guid? Id, string? Name, Guid? CountryId, int? Offset, int? Limit, string? OrderBy) : IRequest<ResultGetList<StateResponse>>;
