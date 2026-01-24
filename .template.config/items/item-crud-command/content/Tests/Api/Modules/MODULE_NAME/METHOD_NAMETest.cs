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
    public async Task METHOD_NAME_Unauthorized_NotSuccessStatusCode()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("Test");

        using var httpContent = ToHttpContent(request);
        var response = await Fixture.ClientNoAuth.PatchAsync(Path + $"{Guid.Empty}/" + MODULE_NAMEConstants.MethodMETHOD_NAME, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task METHOD_NAME_NullRequest_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.PatchAsync(Path + $"{TestsConstants.DefaultValidId}/" + MODULE_NAMEConstants.MethodMETHOD_NAME, null);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task METHOD_NAME_EmptyId_NotSuccessStatusCode()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("Test");

        using var httpContent = ToHttpContent(request);
        var response = await Fixture.ClientAuthAdmin.PatchAsync(Path + $"{Guid.Empty}/" + MODULE_NAMEConstants.MethodMETHOD_NAME, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task METHOD_NAME_InvalidId_NotSuccessStatusCode()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("Test");

        using var httpContent = ToHttpContent(request);
        var response = await Fixture.ClientAuthAdmin.PatchAsync(Path + $"{TestsConstants.DefaultInvalidId}/" + MODULE_NAMEConstants.MethodMETHOD_NAME, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task METHOD_NAME_ValidIdEmptyValue_NotSuccessStatusCode()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("");

        using var httpContent = ToHttpContent(request);
        var response = await Fixture.ClientAuthAdmin.PatchAsync(Path + $"{TestsConstants.DefaultInvalidId}/" + MODULE_NAMEConstants.MethodMETHOD_NAME, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task METHOD_NAME_Valid_SuccessStatusCode()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("Test");

        using var httpContent = ToHttpContent(request);
        var response = await Fixture.ClientAuthAdmin.PatchAsync(Path + $"{TestsConstants.DefaultValidId}/" + MODULE_NAMEConstants.MethodMETHOD_NAME, httpContent);

        Assert.True(response.IsSuccessStatusCode);
    }
}
