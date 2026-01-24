using Deve.Data;
using Deve.Dto.Responses.Results;
using Deve.MODULE_NAME.ITEM_NAME_PLURAL;

namespace Deve.Tests.Modules.MODULE_NAME;

public abstract class METHOD_NAMETest : BaseTest<IData>
{
    protected METHOD_NAMETest(IDataFixture<IData> fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task METHOD_NAME_NoAuthValidData_ReturnNotSuccess()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("Test");

        var res = await Fixture.DataNoAuth.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(TestsConstants.DefaultValidId, request);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task METHOD_NAME_NoAuthValidData_ReturnErrorNotNull()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("Test");

        var res = await Fixture.DataNoAuth.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(TestsConstants.DefaultValidId, request);

        Assert.NotNull(res.Errors);
    }

    [Fact]
    public async Task METHOD_NAME_NoAuthValidData_ReturnErrorNotEmpty()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("Test");

        var res = await Fixture.DataNoAuth.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(TestsConstants.DefaultValidId, request);

        Assert.NotEmpty(res.Errors);
    }

    [Fact]
    public async Task METHOD_NAME_NoAuthValidData_ReturnErrorType()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("Test");

        var res = await Fixture.DataNoAuth.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(TestsConstants.DefaultValidId, request);

        _ = Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
    }

    [Fact]
    public async Task METHOD_NAME_InvalidData_ReturnNotSuccess()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("");

        var res = await Fixture.DataAuthAdmin.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(Guid.Empty, request);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task METHOD_NAME_InvalidData_ReturnErrorsNotNull()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("");

        var res = await Fixture.DataAuthAdmin.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(Guid.Empty, request);

        Assert.NotNull(res.Errors);
    }

    [Fact]
    public async Task METHOD_NAME_InvalidData_ReturnErrorsType()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("");

        var res = await Fixture.DataAuthAdmin.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(Guid.Empty, request);

        _ = Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
    }

    [Fact]
    public async Task METHOD_NAME_InvalidData_ReturnErrorsNotEmpty()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("");

        var res = await Fixture.DataAuthAdmin.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(Guid.Empty, request);

        Assert.NotEmpty(res.Errors);
    }

    [Fact]
    public async Task METHOD_NAME_ValidData_ReturnSuccess()
    {
        var request = new ITEM_NAME_SINGULARMETHOD_NAMERequest("Test");

        var res = await Fixture.DataAuthAdmin.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAMEAsync(TestsConstants.DefaultValidId, request);

        Assert.True(res.Success);
    }
}
