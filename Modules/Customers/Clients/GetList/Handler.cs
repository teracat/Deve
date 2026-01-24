using Deve.Customers.Enums;

namespace Deve.Customers.Clients.GetList;

internal sealed class Handler(
    IRepository<Client> repositoryClient,
    IRepository<City> repositoryCity,
    IRepository<State> repositoryState,
    IRepository<Country> repositoryCountry) : IGetListQueryHandler<Query, ClientListResponse>
{
    public Task<ResultGetList<ClientListResponse>> HandleAsync(Query request, CancellationToken cancellationToken) =>
        Task.Run(() =>
        {
            var query = FullData.CreateQuery(repositoryClient, repositoryCity, repositoryState, repositoryCountry);

            // Apply filters
            if (request.Id.HasValue)
            {
                query = query.Where(x => x.Client.Id == request.Id.Value);
            }
            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(x => x.Client.Name.Contains(request.Name, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(request.TradeName))
            {
                query = query.Where(x => !string.IsNullOrEmpty(x.Client.TradeName) && x.Client.TradeName.Contains(request.TradeName, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(request.TaxId))
            {
                query = query.Where(x => !string.IsNullOrEmpty(x.Client.TaxId) && x.Client.TaxId.Contains(request.TaxId, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(request.TaxName))
            {
                query = query.Where(x => !string.IsNullOrEmpty(x.Client.TaxName) && x.Client.TaxName.Contains(request.TaxName, StringComparison.OrdinalIgnoreCase));
            }
            if (request.CityId.HasValue)
            {
                query = query.Where(x => x.Client.CityId.Equals(request.CityId));
            }
            if (request.StatusFilterType.HasValue)
            {
                query = request.StatusFilterType.Value switch
                {
                    ClientStatusFilterType.OnlyActive => query.Where(x => x.Client.Status == ClientStatus.Active),
                    ClientStatusFilterType.OnlyInactive => query.Where(x => x.Client.Status == ClientStatus.Inactive),
                    ClientStatusFilterType.All => query,
                    _ => query,
                };
            }

            // Get total count before pagination
            int totalCount = query.Count();

            // Apply ordering
            string orderBy = request.OrderBy ?? nameof(ClientGetListRequest.Name);
            query = orderBy.ToUpperInvariant() switch
            {
                "ID" => query.OrderBy(x => x.Client.Id),
                "CITYID" => query.OrderBy(x => x.Client.CityId),
                _ => query.OrderBy(x => x.Client.Name),
            };

            // Apply pagination
            var offset = request?.Offset ?? 0;
            var limit = request?.Limit ?? Constants.DefaultCriteriaLimit;
            var list = query.Select(x => x.ToListResponse())
                            .Skip(offset)
                            .Take(limit)
                            .ToList();

            return Result.OkGetList(list, offset, limit, orderBy, totalCount);
        }, cancellationToken);
}
