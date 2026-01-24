using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Customers.Countries;

public interface ICountryData : IModuleItem
{
    // Queries
    Task<ResultGetList<CountryResponse>> GetAsync() => GetAsync(null, CancellationToken.None);
    Task<ResultGetList<CountryResponse>> GetAsync(CancellationToken cancellationToken) => GetAsync(null, cancellationToken);
    Task<ResultGetList<CountryResponse>> GetAsync(CountryGetListRequest? request) => GetAsync(request, CancellationToken.None);
    Task<ResultGetList<CountryResponse>> GetAsync(CountryGetListRequest? request, CancellationToken cancellationToken);

    Task<ResultGet<CountryResponse>> GetByIdAsync(Guid id) => GetByIdAsync(id, CancellationToken.None);
    Task<ResultGet<CountryResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    // <hooks:data-queries>

    // Commands
    Task<ResultGet<ResponseId>> AddAsync(CountryAddRequest request) => AddAsync(request, CancellationToken.None);
    Task<ResultGet<ResponseId>> AddAsync(CountryAddRequest request, CancellationToken cancellationToken);

    Task<Result> UpdateAsync(Guid id, CountryUpdateRequest request) => UpdateAsync(id, request, CancellationToken.None);
    Task<Result> UpdateAsync(Guid id, CountryUpdateRequest request, CancellationToken cancellationToken);

    Task<Result> DeleteAsync(Guid id) => DeleteAsync(id, CancellationToken.None);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);

    // <hooks:data-commands>
}
