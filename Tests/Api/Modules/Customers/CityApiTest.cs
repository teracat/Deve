using Deve.Customers;
using Deve.Customers.Cities;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api.Modules.Customers;

public class CityApiTest : BaseAllApiTest, IClassFixture<FixtureApiClients>
{
    protected override string Path => CustomersConstants.PathCityV1;

    public CityApiTest(FixtureApiClients fixture)
        : base(fixture)
    {
    }

    protected override object CreateInvalidRequestToAdd() => new CityAddRequest(string.Empty, Guid.Empty);

    protected override object CreateInvalidRequestToUpdate() => new CityUpdateRequest(string.Empty, Guid.Empty);

    protected override object CreateValidRequestToAdd() => new CityAddRequest("Tests City", TestsConstants.BarcelonaStateId);

    protected override object CreateValidRequestToUpdate() => new CityUpdateRequest("Santpedor", TestsConstants.BarcelonaStateId);
}
