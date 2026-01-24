using Deve.Customers;
using Deve.Customers.Countries;
using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Sdk.Customers;

internal class CountrySdk : BaseSdk, ICountryData
{
    private static readonly string Path = CustomersConstants.PathCountryV1;

    public CountrySdk(ISdk sdk)
        : base(sdk)
    {
    }

    // Query
    public async Task<ResultGetList<CountryResponse>> GetAsync(CountryGetListRequest? request, CancellationToken cancellationToken) => await GetList<CountryResponse, CountryGetListRequest>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<ResultGet<CountryResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken) => await Get<CountryResponse>(Path, RequestAuthType.Default, id, cancellationToken);

    // <hooks:sdk-queries>

    // Command
    public async Task<ResultGet<ResponseId>> AddAsync(CountryAddRequest request, CancellationToken cancellationToken) => await PostWithResult<ResponseId>(Path, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> UpdateAsync(Guid id, CountryUpdateRequest request, CancellationToken cancellationToken) => await Put(Path + id, RequestAuthType.Default, request, cancellationToken);
    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken) => await Delete(Path + id, RequestAuthType.Default, cancellationToken);

    // <hooks:sdk-commands>
}
