using Deve.Data;
using Deve.MODULE_NAME;
using Deve.MODULE_NAME.FEATURE_PLURAL;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api.Modules.MODULE_NAME;

public class METHOD_NAMETest : BaseApiTest, IClassFixture<FixtureApiClients>
{
    private static string Path => MODULE_NAMEConstants.PathFEATURE_SINGULARV1;

    public METHOD_NAMETest(FixtureApiClients fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task METHOD_NAME_EmptyRequest_SuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.GetAsync(Path + MODULE_NAMEConstants.MethodMETHOD_NAME);

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task METHOD_NAME_Parameter_SuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.GetAsync(Path + MODULE_NAMEConstants.MethodMETHOD_NAME + $"?{nameof(FEATURE_SINGULARMETHOD_NAMERequest.Name)}=aa");

        Assert.True(response.IsSuccessStatusCode);
    }
}
