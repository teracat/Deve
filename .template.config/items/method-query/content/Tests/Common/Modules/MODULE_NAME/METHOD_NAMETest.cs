using Deve.Data;
using Deve.MODULE_NAME.FEATURE_PLURAL;

namespace Deve.Tests.Modules.MODULE_NAME;

public abstract class METHOD_NAMETest : BaseTest<IData>
{
    protected METHOD_NAMETest(IDataFixture<IData> fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task METHOD_NAME_Null_ReturnResultNotNull()
    {
        var res = await Fixture.DataAuthAdmin.MODULE_NAME.FEATURE_PLURAL.METHOD_NAMEAsync(null);

        Assert.NotNull(res);
    }

    [Fact]
    public async Task METHOD_NAME_Null_ReturnNotSuccess()
    {
        var res = await Fixture.DataAuthAdmin.MODULE_NAME.FEATURE_PLURAL.METHOD_NAMEAsync(null);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task METHOD_NAME_Empty_ReturnNotSuccess()
    {
        var res = await Fixture.DataAuthAdmin.MODULE_NAME.FEATURE_PLURAL.METHOD_NAMEAsync(new FEATURE_SINGULARMETHOD_NAMERequest() { Id = Guid.Empty });

        Assert.False(res.Success);
    }

    [Fact]
    public async Task METHOD_NAME_Empty_ReturnDataNull()
    {
        var res = await Fixture.DataAuthAdmin.MODULE_NAME.FEATURE_PLURAL.METHOD_NAMEAsync(new FEATURE_SINGULARMETHOD_NAMERequest() { Id = Guid.Empty });

        Assert.Null(res.Data);
    }

    [Fact]
    public async Task METHOD_NAME_InvalidId_ReturnNotSuccess()
    {
        var res = await Fixture.DataAuthAdmin.MODULE_NAME.FEATURE_PLURAL.METHOD_NAMEAsync(new FEATURE_SINGULARMETHOD_NAMERequest() { Id = TestsConstants.DefaultInvalidId });

        Assert.False(res.Success);
    }

    [Fact]
    public async Task METHOD_NAME_ValidId_ReturnResultNotNull()
    {
        var res = await Fixture.DataAuthAdmin.MODULE_NAME.FEATURE_PLURAL.METHOD_NAMEAsync(new FEATURE_SINGULARMETHOD_NAMERequest() { Id = TestsConstants.DefaultValidId });

        Assert.NotNull(res);
    }

    [Fact]
    public async Task METHOD_NAME_ValidId_ReturnSuccess()
    {
        var res = await Fixture.DataAuthAdmin.MODULE_NAME.FEATURE_PLURAL.METHOD_NAMEAsync(new FEATURE_SINGULARMETHOD_NAMERequest() { Id = TestsConstants.DefaultValidId });

        Assert.True(res.Success);
    }

    [Fact]
    public async Task METHOD_NAME_ValidId_ReturnDataNotNull()
    {
        var res = await Fixture.DataAuthAdmin.MODULE_NAME.FEATURE_PLURAL.METHOD_NAMEAsync(new FEATURE_SINGULARMETHOD_NAMERequest() { Id = TestsConstants.DefaultValidId });

        Assert.NotNull(res.Data);
    }

    [Fact]
    public async Task METHOD_NAME_NoAuthValidId_ReturnNotSuccess()
    {
        var res = await Fixture.DataNoAuth.MODULE_NAME.FEATURE_PLURAL.METHOD_NAMEAsync(new FEATURE_SINGULARMETHOD_NAMERequest() { Id = TestsConstants.DefaultValidId });

        Assert.False(res.Success);
    }
}
