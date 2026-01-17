using Deve.Data;
using Deve.Dto.Responses.Results;

namespace Deve.Tests;

/// <summary>
/// DataGet methods tests.
/// The ExecuteValidLogin is used to avoid permissions errors.
/// </summary>
public abstract class DataGetBaseTest<TDataType, TReponseList, TReponseGetById> : BaseTest<TDataType> where TDataType : IData
{
    #region Properties
    protected virtual Guid ValidId => TestsConstants.DefaultValidId;
    protected virtual Guid InvalidId => TestsConstants.DefaultInvalidId;
    #endregion

    #region Constructor
    protected DataGetBaseTest(IDataFixture<TDataType> fixture)
        : base(fixture)
    {
    }
    #endregion

    #region Abstract Methods
    protected abstract object CreateRequestGetList();
    protected abstract Task<ResultGetList<TReponseList>> GetListAsync(TDataType data, object? request);

    protected abstract Task<ResultGet<TReponseGetById>> GetByIdAsync(TDataType data, Guid? id);
    #endregion

    #region GetList
    [Fact]
    public async Task GetList_RequestNull_ReturnNotNull()
    {
        var res = await GetListAsync(Fixture.DataAuthAdmin, null);

        Assert.NotNull(res);
    }

    [Fact]
    public async Task GetList_RequestNull_ReturnSuccess()
    {
        var res = await GetListAsync(Fixture.DataAuthAdmin, null);

        Assert.True(res.Success);
    }

    [Fact]
    public async Task GetList_RequestNull_ReturnDataType()
    {
        var res = await GetListAsync(Fixture.DataAuthAdmin, null);

        _ = Assert.IsType<IReadOnlyList<TReponseList>>(res.Data, exactMatch: false);
    }

    [Fact]
    public async Task GetList_RequestNull_ReturnNotEmpty()
    {
        var res = await GetListAsync(Fixture.DataAuthAdmin, null);

        Assert.NotEmpty(res.Data);
    }

    [Fact]
    public async Task GetList_RequestNull_ReturnFirstItemNotNull()
    {
        var res = await GetListAsync(Fixture.DataAuthAdmin, null);

        Assert.NotNull(res.Data[0]);
    }
    #endregion

    #region Get
    [Fact]
    public async Task Get_Empty_ReturnResultNotNull()
    {
        var res = await GetByIdAsync(Fixture.DataAuthAdmin, Guid.Empty);

        Assert.NotNull(res);
    }

    [Fact]
    public async Task Get_Empty_ReturnNotSuccess()
    {
        var res = await GetByIdAsync(Fixture.DataAuthAdmin, Guid.Empty);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task Get_Empty_ReturnDataNull()
    {
        var res = await GetByIdAsync(Fixture.DataAuthAdmin, Guid.Empty);

        Assert.Null(res.Data);
    }

    [Fact]
    public async Task Get_InvalidId_ReturnNotSuccess()
    {
        var res = await GetByIdAsync(Fixture.DataAuthAdmin, InvalidId);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task Get_ValidId_ReturnResultNotNull()
    {
        var res = await GetByIdAsync(Fixture.DataAuthAdmin, ValidId);

        Assert.NotNull(res);
    }

    [Fact]
    public async Task Get_ValidId_ReturnSuccess()
    {
        var res = await GetByIdAsync(Fixture.DataAuthAdmin, ValidId);

        Assert.True(res.Success);
    }

    [Fact]
    public async Task Get_ValidId_ReturnDataNotNull()
    {
        var res = await GetByIdAsync(Fixture.DataAuthAdmin, ValidId);

        Assert.NotNull(res.Data);
    }
    #endregion
}
