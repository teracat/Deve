using Deve.Data;
using Deve.Dto.Responses.Results;
using Deve.Customers.Stats;

namespace Deve.Tests.Modules.Customers;

public abstract class StatsTest : BaseTest<IData>
{
    #region Constructor
    protected StatsTest(IDataFixture<IData> fixture)
        : base(fixture)
    {
    }
    #endregion

    #region Tests
    [Fact]
    public async Task GetClientStats_NoAuth_ReturnNotSuccess()
    {
        var res = await Fixture.DataNoAuth.Customers.Stats.GetClientStatsAsync();

        Assert.False(res.Success);
    }

    [Fact]
    public async Task GetClientStats_NoAuth_ReturnErrorNotNull()
    {
        var res = await Fixture.DataNoAuth.Customers.Stats.GetClientStatsAsync();

        Assert.NotNull(res.Errors);
    }

    [Fact]
    public async Task GetClientStats_NoAuth_ReturnErrorNotEmpty()
    {
        var res = await Fixture.DataNoAuth.Customers.Stats.GetClientStatsAsync();

        Assert.NotEmpty(res.Errors);
    }

    [Fact]
    public async Task GetClientStats_NoAuth_ReturnErrorType()
    {
        var res = await Fixture.DataNoAuth.Customers.Stats.GetClientStatsAsync();

        _ = Assert.IsType<IReadOnlyList<ResultError>>(res.Errors, exactMatch: false);
    }

    [Fact]
    public async Task GetClientStats_AuthAdmin_ReturnSuccess()
    {
        var res = await Fixture.DataAuthAdmin.Customers.Stats.GetClientStatsAsync();

        Assert.True(res.Success);
    }

    [Fact]
    public async Task GetClientStats_AuthAdmin_ReturnDataNotNull()
    {
        var res = await Fixture.DataAuthAdmin.Customers.Stats.GetClientStatsAsync();

        Assert.NotNull(res.Data);
    }

    [Fact]
    public async Task GetClientStats_AuthAdmin_ReturnDataType()
    {
        var res = await Fixture.DataAuthAdmin.Customers.Stats.GetClientStatsAsync();

        _ = Assert.IsType<ClientStatsResponse>(res.Data, exactMatch: false);
    }
    #endregion
}
