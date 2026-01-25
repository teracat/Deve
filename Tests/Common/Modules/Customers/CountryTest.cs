using Deve.Data;
using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;
using Deve.Customers.Countries;

namespace Deve.Tests.Modules.Customers;

public abstract class CountryTest : DataAllBaseTest<IData, CountryResponse, CountryResponse>
{
    #region Constructor
    protected CountryTest(IDataFixture<IData> fixture)
        : base(fixture)
    {
    }
    #endregion

    #region Overrides
    protected override object CreateRequestGetList() => new CountryGetListRequest();

    protected override object CreateInvalidRequestToAdd() => new CountryAddRequest
    {
        Name = string.Empty,
        IsoCode = string.Empty
    };

    protected override object CreateInvalidRequestToUpdate() => new CountryUpdateRequest
    {
        Name = string.Empty,
        IsoCode = string.Empty
    };

    protected override object CreateValidRequestToAdd() => new CountryAddRequest
    {
        Name = "Tests Country",
        IsoCode = "TE"
    };

    protected override object CreateValidRequestToUpdate() => new CountryUpdateRequest
    {
        Name = "España",
        IsoCode = "ES"
    };

    protected override Task<ResultGetList<CountryResponse>> GetListAsync(IData data, object? request) => data.Customers.Countries.GetAsync((CountryGetListRequest?)request);
    protected override Task<ResultGet<CountryResponse>> GetByIdAsync(IData data, Guid? id) => data.Customers.Countries.GetByIdAsync(id ?? Guid.Empty);

    protected override Task<ResultGet<ResponseId>> AddAsync(IData data, object? request) => data.Customers.Countries.AddAsync((CountryAddRequest)request);
    protected override Task<Result> UpdateAsync(IData data, Guid id, object? request) => data.Customers.Countries.UpdateAsync(id, (CountryUpdateRequest)request);
    protected override Task<Result> DeleteAsync(IData data, Guid id) => data.Customers.Countries.DeleteAsync(id);
    #endregion
}
