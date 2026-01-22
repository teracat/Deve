using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Customers.Countries;

public interface ICountryData : IModuleItem
{
    // Queries
    Task<ResultGetList<CountryResponse>> GetAsync(CountryGetListRequest? request, CancellationToken cancellationToken = default);
    Task<ResultGetList<CountryResponse>> GetAsync(CancellationToken cancellationToken = default);
    Task<ResultGet<CountryResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    // <hooks:data-queries>

    // Commands
    Task<ResultGet<ResponseId>> AddAsync(CountryAddRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Guid id, CountryUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    // <hooks:data-commands>
}
