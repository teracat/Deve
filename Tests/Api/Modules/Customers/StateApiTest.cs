using Deve.Customers.States;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api.Modules.Customers;

public class StateApiTest : BaseAllApiTest, IClassFixture<FixtureApiClients>
{
    protected override string Path => ApiConstants.PathStateV1;

    public StateApiTest(FixtureApiClients fixture)
        : base(fixture)
    {
    }

    protected override object CreateInvalidRequestToAdd() => new StateAddRequest(string.Empty, Guid.Empty);

    protected override object CreateInvalidRequestToUpdate() => new StateUpdateRequest(string.Empty, Guid.Empty);

    protected override object CreateValidRequestToAdd() => new StateAddRequest
    (
        Name: "Tests State",
        CountryId: TestsConstants.SpainCountryId
    );

    protected override object CreateValidRequestToUpdate() => new StateUpdateRequest
    (
        Name: "Barcelona",
        CountryId: TestsConstants.SpainCountryId
    );
}
