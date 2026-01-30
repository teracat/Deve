using Deve.MODULE_NAME;
using Deve.MODULE_NAME.ITEM_NAME_PLURAL;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api.Modules.MODULE_NAME;

public class METHOD_NAMETest : BaseApiTest, IClassFixture<FixtureApiClients>
{
    private static string Path => MODULE_NAMEConstants.PathITEM_NAME_SINGULARV1;

    public METHOD_NAMETest(FixtureApiClients fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task METHOD_NAME_Empty_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.GetAsync(Path + MODULE_NAMEConstants.MethodMETHOD_NAME + $"?{nameof(ITEM_NAME_SINGULARMETHOD_NAMERequest.Id)}={Guid.Empty}");

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task METHOD_NAME_ValidId_SuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.GetAsync(Path + MODULE_NAMEConstants.MethodMETHOD_NAME + $"?{nameof(ITEM_NAME_SINGULARMETHOD_NAMERequest.Id)}={TestsConstants.DefaultValidId}");

        Assert.True(response.IsSuccessStatusCode);
    }
}
