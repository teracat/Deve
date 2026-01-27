using Deve.Data;
using Deve.MODULE_NAME.ITEM_NAME_PLURAL;

namespace Deve.Tests.Modules.MODULE_NAME;

public abstract class METHOD_NAMETest : BaseTest<IData>
{
    protected METHOD_NAMETest(IDataFixture<IData> fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task METHOD_NAME_RequestNull_ReturnNotNull()
    {
        var res = await Fixture.DataAuthAdmin.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(null);

        Assert.NotNull(res);
    }

    [Fact]
    public async Task METHOD_NAME_RequestNull_ReturnSuccess()
    {
        var res = await Fixture.DataAuthAdmin.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(null);

        Assert.True(res.Success);
    }

    [Fact]
    public async Task METHOD_NAME_RequestNull_ReturnDataType()
    {
        var res = await Fixture.DataAuthAdmin.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(null);

        _ = Assert.IsType<IReadOnlyList<ITEM_NAME_SINGULARMETHOD_NAMEResponse>>(res.Data, exactMatch: false);
    }

    [Fact]
    public async Task METHOD_NAME_RequestNull_ReturnNotEmpty()
    {
        var res = await Fixture.DataAuthAdmin.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(null);

        Assert.NotEmpty(res.Data);
    }

    [Fact]
    public async Task METHOD_NAME_RequestNull_ReturnFirstItemNotNull()
    {
        var res = await Fixture.DataAuthAdmin.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(null);

        Assert.NotNull(res.Data[0]);
    }
}
