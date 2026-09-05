using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api;

public abstract class BaseAllApiTest : BaseGetApiTest
{
    #region Properties
    protected virtual Guid ValidIdDelete => TestsConstants.DefaultValidIdDelete;
    #endregion

    #region Constructor
    protected BaseAllApiTest(FixtureApiClients fixture)
        : base(fixture)
    {
    }
    #endregion

    #region Abstract Methods
    protected abstract object CreateInvalidRequestToAdd();
    protected abstract object CreateInvalidRequestToUpdate();
    protected abstract object CreateValidRequestToAdd();
    protected abstract object CreateValidRequestToUpdate();
    #endregion

    #region Add
    [Fact]
    public async Task Add_Unauthorized_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientNoAuth.PostAsync(Path, null, TestContext.Current.CancellationToken);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Add_NullRequestBody_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.PostAsync(Path, null, TestContext.Current.CancellationToken);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Add_InvalidData_NotSuccessStatusCode()
    {
        var data = CreateInvalidRequestToAdd();
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientAuthAdmin.PostAsync(Path, httpContent, TestContext.Current.CancellationToken);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Add_ValidData_SuccessStatusCode()
    {
        var data = CreateValidRequestToAdd();
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientAuthAdmin.PostAsync(Path, httpContent, TestContext.Current.CancellationToken);

        Assert.True(response.IsSuccessStatusCode);
    }
    #endregion

    #region Update
    [Fact]
    public async Task Update_Unauthorized_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientNoAuth.PutAsync(Path + Guid.Empty, null, TestContext.Current.CancellationToken);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Update_NullRequestBody_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.PutAsync(Path + Guid.Empty, null, TestContext.Current.CancellationToken);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Update_InvalidData_NotSuccessStatusCode()
    {
        var data = CreateInvalidRequestToUpdate();
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientAuthAdmin.PutAsync(Path + Guid.Empty, httpContent, TestContext.Current.CancellationToken);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Update_ValidData_SuccessStatusCode()
    {
        var data = CreateValidRequestToUpdate();
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientAuthAdmin.PutAsync(Path + ValidId, httpContent, TestContext.Current.CancellationToken);

        Assert.True(response.IsSuccessStatusCode);
    }
    #endregion

    #region Delete
    [Fact]
    public async Task Delete_Unauthorized_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientNoAuth.DeleteAsync(Path + Guid.Empty, TestContext.Current.CancellationToken);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Delete_ZeroId_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.DeleteAsync(Path + Guid.Empty, TestContext.Current.CancellationToken);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Delete_InvalidId_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.DeleteAsync(Path + InvalidId, TestContext.Current.CancellationToken);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Delete_ValidId_SuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.DeleteAsync(Path + ValidIdDelete, TestContext.Current.CancellationToken);

        Assert.True(response.IsSuccessStatusCode);
    }
    #endregion
}
