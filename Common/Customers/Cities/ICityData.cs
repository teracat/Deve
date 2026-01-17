using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Customers.Cities;

public interface ICityData : IModuleItem
{
    // Queries
    Task<ResultGetList<CityResponse>> GetAsync(CityGetListRequest? request, CancellationToken cancellationToken = default);
    Task<ResultGetList<CityResponse>> GetAsync(CancellationToken cancellationToken = default);
    Task<ResultGet<CityResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    // Commands
    Task<ResultGet<ResponseId>> AddAsync(CityAddRequest request, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(Guid id, CityUpdateRequest request, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
