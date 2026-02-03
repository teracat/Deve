using Deve.MODULE_NAME;
using Deve.MODULE_NAME.FEATURE_PLURAL;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api.Modules.MODULE_NAME;

public class FEATURE_SINGULARApiTest : BaseAllApiTest, IClassFixture<FixtureApiClients>
{
    protected override string Path => MODULE_NAMEConstants.PathFEATURE_SINGULARV1;

    public FEATURE_SINGULARApiTest(FixtureApiClients fixture)
        : base(fixture)
    {
    }

    protected override object CreateInvalidRequestToAdd() => new FEATURE_SINGULARAddRequest
    {
        Name = string.Empty
    };

    protected override object CreateInvalidRequestToUpdate() => new FEATURE_SINGULARUpdateRequest
    {
        Name = string.Empty
    };

    protected override object CreateValidRequestToAdd() => new FEATURE_SINGULARAddRequest
    {
        Name = "Add Tests"
    };

    protected override object CreateValidRequestToUpdate() => new FEATURE_SINGULARUpdateRequest
    {
        Name = "Update Test"
    };
}
