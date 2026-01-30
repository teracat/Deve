using Deve.Data;
using Deve.Dto.Responses;
using Deve.Dto.Responses.Results;

namespace Deve.Tests;

/// <summary>
/// DataAll methods tests.
/// The ExecuteValidLogin is used to avoid permissions errors.
/// </summary>
public abstract class DataAllBaseTest<TDataType, TReponseList, TReponseGetById> : DataGetBaseTest<TDataType, TReponseList, TReponseGetById> where TDataType : IData
{
    #region Abstract Methods
    protected abstract object CreateInvalidRequestToAdd();
    protected abstract object CreateInvalidRequestToUpdate();
    protected abstract object CreateValidRequestToAdd();
    protected abstract object CreateValidRequestToUpdate();

    protected abstract Task<ResultGet<ResponseId>> AddAsync(TDataType data, object? request);
    protected abstract Task<Result> UpdateAsync(TDataType data, Guid id, object? request);
    protected abstract Task<Result> DeleteAsync(TDataType data, Guid id);
    #endregion

    #region Properties
    protected virtual Guid ValidIdDelete => TestsConstants.DefaultValidIdDelete;
    #endregion

    #region Constructor
    protected DataAllBaseTest(IDataFixture<TDataType> fixture)
        : base(fixture)
    {
    }
    #endregion

    #region Add
    [Fact]
    public async Task Add_NoAuthValidData_ReturnNotSuccess()
    {
        var obj = CreateValidRequestToAdd();

        var res = await AddAsync(Fixture.DataNoAuth, obj);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task Add_NoAuthValidData_ReturnErrorNotNull()
    {
        var obj = CreateValidRequestToAdd();

        var res = await AddAsync(Fixture.DataNoAuth, obj);

        Assert.NotNull(res.Errors);
    }

    [Fact]
    public async Task Add_NoAuthValidData_ReturnErrorNotEmpty()
    {
        var obj = CreateValidRequestToAdd();

        var res = await AddAsync(Fixture.DataNoAuth, obj);

        Assert.NotEmpty(res.Errors);
    }

    [Fact]
    public async Task Add_NoAuthValidData_ReturnErrorType()
    {
        var obj = CreateValidRequestToAdd();

        var res = await AddAsync(Fixture.DataNoAuth, obj);

        _ = Assert.IsType<IList<ResultError>>(res.Errors, exactMatch: false);
    }

    [Fact]
    public async Task Add_Null_ReturnNotSuccess()
    {
        var res = await AddAsync(Fixture.DataAuthAdmin, null);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task Add_InvalidData_ReturnNotSuccess()
    {
        var obj = CreateInvalidRequestToAdd();

        var res = await AddAsync(Fixture.DataAuthAdmin, obj);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task Add_InvalidData_ReturnErrorsNotNull()
    {
        var obj = CreateInvalidRequestToAdd();

        var res = await AddAsync(Fixture.DataAuthAdmin, obj);

        Assert.NotNull(res.Errors);
    }

    [Fact]
    public async Task Add_InvalidData_ReturnErrorsType()
    {
        var obj = CreateInvalidRequestToAdd();

        var res = await AddAsync(Fixture.DataAuthAdmin, obj);

        _ = Assert.IsType<IList<ResultError>>(res.Errors, exactMatch: false);
    }

    [Fact]
    public async Task Add_InvalidData_ReturnErrorsNotEmpty()
    {
        var obj = CreateInvalidRequestToAdd();

        var res = await AddAsync(Fixture.DataAuthAdmin, obj);

        Assert.NotEmpty(res.Errors);
    }

    [Fact]
    public async Task Add_ValidData_ReturnSuccess()
    {
        var obj = CreateValidRequestToAdd();

        var res = await AddAsync(Fixture.DataAuthAdmin, obj);

        Assert.True(res.Success, res.Errors?.FirstOrDefault()?.Description);
    }
    #endregion

    #region Update
    [Fact]
    public async Task Update_NoAuthValidData_ReturnNotSuccess()
    {
        var obj = CreateValidRequestToUpdate();

        var res = await UpdateAsync(Fixture.DataNoAuth, ValidId, obj);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task Update_NoAuthValidData_ReturnErrorNotNull()
    {
        var obj = CreateValidRequestToUpdate();

        var res = await UpdateAsync(Fixture.DataNoAuth, ValidId, obj);

        Assert.NotNull(res.Errors);
    }

    [Fact]
    public async Task Update_NoAuthValidData_ReturnErrorNotEmpty()
    {
        var obj = CreateValidRequestToUpdate();

        var res = await UpdateAsync(Fixture.DataNoAuth, ValidId, obj);

        Assert.NotEmpty(res.Errors);
    }

    [Fact]
    public async Task Update_NoAuthValidData_ReturnErrorType()
    {
        var obj = CreateValidRequestToUpdate();

        var res = await UpdateAsync(Fixture.DataNoAuth, ValidId, obj);

        _ = Assert.IsType<IList<ResultError>>(res.Errors, exactMatch: false);
    }

    [Fact]
    public async Task Update_Null_ReturnNotSuccess()
    {
        var res = await UpdateAsync(Fixture.DataAuthAdmin, ValidId, null);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task Update_InvalidData_ReturnNotSuccess()
    {
        var obj = CreateInvalidRequestToUpdate();

        var res = await UpdateAsync(Fixture.DataAuthAdmin, ValidId, obj);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task Update_InvalidData_ReturnErrorsNotNull()
    {
        var obj = CreateInvalidRequestToUpdate();

        var res = await UpdateAsync(Fixture.DataAuthAdmin, ValidId, obj);

        Assert.NotNull(res.Errors);
    }

    [Fact]
    public async Task Update_InvalidData_ReturnErrorsType()
    {
        var obj = CreateInvalidRequestToUpdate();

        var res = await UpdateAsync(Fixture.DataAuthAdmin, ValidId, obj);

        _ = Assert.IsType<IList<ResultError>>(res.Errors, exactMatch: false);
    }

    [Fact]
    public async Task Update_InvalidData_ReturnErrorsNotEmpty()
    {
        var obj = CreateInvalidRequestToUpdate();

        var res = await UpdateAsync(Fixture.DataAuthAdmin, ValidId, obj);

        Assert.NotEmpty(res.Errors);
    }

    [Fact]
    public async Task Update_ValidData_ReturnSuccess()
    {
        var obj = CreateValidRequestToUpdate();

        var res = await UpdateAsync(Fixture.DataAuthAdmin, ValidId, obj);

        Assert.True(res.Success, res.Errors?.FirstOrDefault()?.Description);
    }
    #endregion

    #region Delete
    [Fact]
    public async Task Delete_NoAuthValidData_ReturnNotSuccess()
    {
        var res = await DeleteAsync(Fixture.DataNoAuth, ValidIdDelete);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task Delete_NoAuthValidData_ReturnErrorNotNull()
    {
        var res = await DeleteAsync(Fixture.DataNoAuth, ValidIdDelete);

        Assert.NotNull(res.Errors);
    }

    [Fact]
    public async Task Delete_NoAuthValidData_ReturnErrorNotEmpty()
    {
        var res = await DeleteAsync(Fixture.DataNoAuth, ValidIdDelete);

        Assert.NotEmpty(res.Errors);
    }

    [Fact]
    public async Task Delete_NoAuthValidData_ReturnErrorType()
    {
        var res = await DeleteAsync(Fixture.DataNoAuth, ValidIdDelete);

        _ = Assert.IsType<IReadOnlyList<ResultError>>(res.Errors, exactMatch: false);
    }

    [Fact]
    public async Task Delete_Empty_ReturnNotSuccess()
    {
        var res = await DeleteAsync(Fixture.DataAuthAdmin, Guid.Empty);

        Assert.False(res.Success);
    }

    [Fact]
    public async Task Delete_Empty_ReturnErrorsNotNull()
    {
        var res = await DeleteAsync(Fixture.DataAuthAdmin, Guid.Empty);

        Assert.NotNull(res.Errors);
    }

    [Fact]
    public async Task Delete_Empty_ReturnErrorsType()
    {
        var res = await DeleteAsync(Fixture.DataAuthAdmin, Guid.Empty);

        _ = Assert.IsType<IReadOnlyList<ResultError>>(res.Errors, exactMatch: false);
    }

    [Fact]
    public async Task Delete_Empty_ReturnErrorsNotEmpty()
    {
        var res = await DeleteAsync(Fixture.DataAuthAdmin, Guid.Empty);

        Assert.NotEmpty(res.Errors);
    }

    [Fact]
    public async Task Delete_InvalidId_ReturnNotSuccess()
    {
        var res = await DeleteAsync(Fixture.DataAuthAdmin, InvalidId);

        Assert.NotEmpty(res.Errors);
    }

    [Fact]
    public async Task Delete_ValidData_ReturnSuccess()
    {
        var res = await DeleteAsync(Fixture.DataAuthAdmin, ValidIdDelete);

        Assert.True(res.Success);
    }
    #endregion
}
