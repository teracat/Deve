using Deve.Customers;
using Deve.Customers.Countries;
using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Sdk.Customers;

internal class CountrySdk : BaseSdk, ICountryData
{
    private const string Path = CustomersConstants.PathCountryV1;

    public CountrySdk(ISdk sdk)
        : base(sdk)
    {
    }

    // Query
    public async Task<ResultGetList<CountryResponse>> GetAsync(CountryGetListRequest? request, CancellationToken cancellationToken = default) => await GetList<CountryResponse, CountryGetListRequest>(Path, request, RequestAuthType.Default, cancellationToken);
    public async Task<ResultGetList<CountryResponse>> GetAsync(CancellationToken cancellationToken = default) => await GetAsync(default, cancellationToken);
    public async Task<ResultGet<CountryResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => await Get<CountryResponse>(Path, RequestAuthType.Default, id, cancellationToken);

    // <hooks:sdk-queries>

    // Command
    public async Task<ResultGet<ResponseId>> AddAsync(CountryAddRequest request, CancellationToken cancellationToken = default) => await PostWithResult<ResponseId>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> UpdateAsync(Guid id, CountryUpdateRequest request, CancellationToken cancellationToken = default) => await Put(Path + id, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default) => await Delete(Path + id, RequestAuthType.Default, cancellationToken);

    // <hooks:sdk-commands>
}
