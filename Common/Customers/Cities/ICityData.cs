using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Customers.Cities;

public interface ICityData : IModuleItem
{
    // Queries
    Task<ResultGetList<CityResponse>> GetAsync() => GetAsync(null, CancellationToken.None);
    Task<ResultGetList<CityResponse>> GetAsync(CancellationToken cancellationToken) => GetAsync(null, cancellationToken);
    Task<ResultGetList<CityResponse>> GetAsync(CityGetListRequest? request) => GetAsync(request, CancellationToken.None);
    Task<ResultGetList<CityResponse>> GetAsync(CityGetListRequest? request, CancellationToken cancellationToken);

    Task<ResultGet<CityResponse>> GetByIdAsync(Guid id) => GetByIdAsync(id, CancellationToken.None);
    Task<ResultGet<CityResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    // <hooks:data-queries>

    // Commands
    Task<ResultGet<ResponseId>> AddAsync(CityAddRequest request) => AddAsync(request, CancellationToken.None);
    Task<ResultGet<ResponseId>> AddAsync(CityAddRequest request, CancellationToken cancellationToken);

    Task<Result> UpdateAsync(Guid id, CityUpdateRequest request) => UpdateAsync(id, request, CancellationToken.None);
    Task<Result> UpdateAsync(Guid id, CityUpdateRequest request, CancellationToken cancellationToken);

    Task<Result> DeleteAsync(Guid id) => DeleteAsync(id, CancellationToken.None);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);

    // <hooks:data-commands>
}
