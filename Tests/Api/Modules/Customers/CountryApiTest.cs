using Deve.Customers;
using Deve.Customers.Countries;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api.Modules.Customers;

public class CountryApiTest : BaseAllApiTest, IClassFixture<FixtureApiClients>
{
    protected override string Path => CustomersConstants.PathCountryV1;

    public CountryApiTest(FixtureApiClients fixture)
        : base(fixture)
    {
    }

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
        IsoCode= "TE"
    };

    protected override object CreateValidRequestToUpdate() => new CountryUpdateRequest
    {
        Name = "España",
        IsoCode = "ES"
    };
}
