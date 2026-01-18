using Deve.Customers;
using Deve.Customers.Cities;
using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Sdk.Customers;

internal class CitySdk : BaseSdk, ICityData
{
    private const string Path = CustomersConstants.PathCityV1;

    public CitySdk(ISdk sdk)
        : base(sdk)
    {
    }

    // Query
    public async Task<ResultGetList<CityResponse>> GetAsync(CityGetListRequest? request, CancellationToken cancellationToken = default) => await GetList<CityResponse, CityGetListRequest>(Path, request, RequestAuthType.Default, cancellationToken);
    public async Task<ResultGetList<CityResponse>> GetAsync(CancellationToken cancellationToken = default) => await GetAsync(default, cancellationToken);
    public async Task<ResultGet<CityResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => await Get<CityResponse>(Path, RequestAuthType.Default, id, cancellationToken);

    // Command
    public async Task<ResultGet<ResponseId>> AddAsync(CityAddRequest request, CancellationToken cancellationToken = default) => await PostWithResult<ResponseId>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> UpdateAsync(Guid id, CityUpdateRequest request, CancellationToken cancellationToken = default) => await Put(Path + id, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default) => await Delete(Path + id, RequestAuthType.Default, cancellationToken);
}
