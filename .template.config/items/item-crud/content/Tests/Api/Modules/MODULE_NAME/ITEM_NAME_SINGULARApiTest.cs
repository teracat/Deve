using Deve.MODULE_NAME;
using Deve.MODULE_NAME.ITEM_NAME_PLURAL;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api.Modules.MODULE_NAME;

public class ITEM_NAME_SINGULARApiTest : BaseAllApiTest, IClassFixture<FixtureApiClients>
{
    protected override string Path => MODULE_NAMEConstants.PathITEM_NAME_SINGULARV1;

    public ITEM_NAME_SINGULARApiTest(FixtureApiClients fixture)
        : base(fixture)
    {
    }

    protected override object CreateInvalidRequestToAdd() => new ITEM_NAME_SINGULARAddRequest
    (
        Name: string.Empty
    );

    protected override object CreateInvalidRequestToUpdate() => new ITEM_NAME_SINGULARUpdateRequest
    (
        Name: string.Empty    );

    protected override object CreateValidRequestToAdd() => new ITEM_NAME_SINGULARAddRequest
    (
        Name: "Add Tests"
    );

    protected override object CreateValidRequestToUpdate() => new ITEM_NAME_SINGULARUpdateRequest
    (
        Name: "Update Test"
    );
}
