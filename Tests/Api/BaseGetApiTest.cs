using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api;

public abstract class BaseGetApiTest : BaseApiTest
{
    #region Properties
    protected abstract string Path { get; }

    protected virtual string ValidGetListRequestParameterName => "name";
    protected virtual Guid ValidId => TestsConstants.DefaultValidId;
    protected virtual Guid InvalidId => TestsConstants.DefaultInvalidId;
    #endregion

    #region Constructor
    protected BaseGetApiTest(FixtureApiClients fixture)
        : base(fixture)
    {
    }
    #endregion

    #region GetList
    [Fact]
    public async Task GetList_EmptyRequest_SuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.GetAsync(Path, TestContext.Current.CancellationToken);

        Assert.True(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetList_Parameter_SuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.GetAsync(Path + $"?{ValidGetListRequestParameterName}=aa", TestContext.Current.CancellationToken);

        Assert.True(response.IsSuccessStatusCode);
    }
    #endregion

    #region Get
    [Fact]
    public async Task Get_Empty_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.GetAsync(Path + Guid.Empty, TestContext.Current.CancellationToken);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Get_ValidId_SuccessStatusCode()
    {
        var response = await Fixture.ClientAuthAdmin.GetAsync(Path + ValidId, TestContext.Current.CancellationToken);

        Assert.True(response.IsSuccessStatusCode);
    }
    #endregion
}
