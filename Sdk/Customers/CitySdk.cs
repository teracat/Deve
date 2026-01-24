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
    public async Task<ResultGetList<CityResponse>> GetAsync(CityGetListRequest? request, CancellationToken cancellationToken) => await GetList<CityResponse, CityGetListRequest>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<ResultGet<CityResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken) => await Get<CityResponse>(Path, RequestAuthType.Default, id, cancellationToken);

    // <hooks:sdk-queries>

    // Command
    public async Task<ResultGet<ResponseId>> AddAsync(CityAddRequest request, CancellationToken cancellationToken) => await PostWithResult<ResponseId>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> UpdateAsync(Guid id, CityUpdateRequest request, CancellationToken cancellationToken) => await Put(Path + id, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken) => await Delete(Path + id, RequestAuthType.Default, cancellationToken);

    // <hooks:sdk-commands>
}
