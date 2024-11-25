using Deve.Core;
using Deve.External;
using Deve.Internal;

namespace Deve.Tests.Core
{
    /// <summary>
    /// DataAll methods tests.
    /// The ExecuteValidLogin is used to avoid permissions errors.
    /// </summary>
    public abstract class TestCoreBaseDataAll<ModelList, Model, Criteria> : TestCoreBaseDataGet<ModelList, Model, Criteria> where Model: ModelId
    {
        #region Abstract Methods
        protected abstract IDataAll<ModelList, Model, Criteria> GetDataAll(ICore core);

        protected abstract Model CreateInvalidDataToAdd();
        protected abstract Model CreateInvalidDataToUpdate();
        protected abstract Model CreateValidDataToAdd();
        protected abstract Model CreateValidDataToUpdate();
        #endregion

        #region Properties
        /// <summary>
        /// Id used in the Delete methods tests as a valid Id to be deleted.
        /// Should be different from the ValidId defined in TestCoreBaseDataGet and different from the Id used for Update methods.
        /// This Id should not be used in the data used to Add or Update in other tests.
        /// </summary>
        protected virtual long ValidIdDelete => TestsConstants.DefaultValidIdDelete;
        #endregion

        #region Override Methods
        protected override IDataGet<ModelList, Model, Criteria> GetDataGet(ICore core) => GetDataAll(core);
        #endregion

        #region Add
        [Fact]
        public async Task Add_NoAuthValidData_ReturnNotSuccess()
        {
            var core = CreateCore();
            var dataAll = GetDataAll(core);
            var data = CreateValidDataToAdd();

            var res = await dataAll.Add(data);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Add_NoAuthValidData_ReturnErrorNotNull()
        {
            var core = CreateCore();
            var dataAll = GetDataAll(core);
            var data = CreateValidDataToAdd();

            var res = await dataAll.Add(data);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task Add_NoAuthValidData_ReturnErrorNotEmpty()
        {
            var core = CreateCore();
            var dataAll = GetDataAll(core);
            var data = CreateValidDataToAdd();

            var res = await dataAll.Add(data);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Add_NoAuthValidData_ReturnErrorType()
        {
            var core = CreateCore();
            var dataAll = GetDataAll(core);
            var data = CreateValidDataToAdd();

            var res = await dataAll.Add(data);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task Add_Null_ReturnNotSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var res = await dataAll.Add(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Add_InvalidData_ReturnNotSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);
            var data = CreateInvalidDataToAdd();

            var res = await dataAll.Add(data);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Add_InvalidData_ReturnErrorsNotNull()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);
            var data = CreateInvalidDataToAdd();

            var res = await dataAll.Add(data);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task Add_InvalidData_ReturnErrorsType()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);
            var data = CreateInvalidDataToAdd();

            var res = await dataAll.Add(data);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task Add_InvalidData_ReturnErrorsNotEmpty()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);
            var data = CreateInvalidDataToAdd();

            var res = await dataAll.Add(data);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Add_ValidData_ReturnSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);
            var data = CreateValidDataToAdd();

            var res = await dataAll.Add(data);

            Assert.True(res.Success, res.Errors?.FirstOrDefault()?.Description);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_NoAuthValidData_ReturnNotSuccess()
        {
            var core = CreateCore();
            var dataAll = GetDataAll(core);
            var data = CreateValidDataToUpdate();

            var res = await dataAll.Update(data);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Update_NoAuthValidData_ReturnErrorNotNull()
        {
            var core = CreateCore();
            var dataAll = GetDataAll(core);
            var data = CreateValidDataToUpdate();

            var res = await dataAll.Update(data);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task Update_NoAuthValidData_ReturnErrorNotEmpty()
        {
            var core = CreateCore();
            var dataAll = GetDataAll(core);
            var data = CreateValidDataToUpdate();

            var res = await dataAll.Update(data);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Update_NoAuthValidData_ReturnErrorType()
        {
            var core = CreateCore();
            var dataAll = GetDataAll(core);
            var data = CreateValidDataToUpdate();

            var res = await dataAll.Update(data);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task Update_Null_ReturnNotSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var res = await dataAll.Update(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Update_InvalidData_ReturnNotSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);
            var data = CreateInvalidDataToUpdate();

            var res = await dataAll.Update(data);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Update_InvalidData_ReturnErrorsNotNull()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);
            var data = CreateInvalidDataToUpdate();

            var res = await dataAll.Update(data);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task Update_InvalidData_ReturnErrorsType()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);
            var data = CreateInvalidDataToUpdate();

            var res = await dataAll.Update(data);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task Update_InvalidData_ReturnErrorsNotEmpty()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);
            var data = CreateInvalidDataToUpdate();

            var res = await dataAll.Update(data);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Update_ValidData_ReturnSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);
            var data = CreateValidDataToUpdate();

            var res = await dataAll.Update(data);

            Assert.True(res.Success, res.Errors?.FirstOrDefault()?.Description);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_NoAuthValidData_ReturnNotSuccess()
        {
            var core = CreateCore();
            var dataAll = GetDataAll(core);

            var res = await dataAll.Delete(ValidIdDelete);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Delete_NoAuthValidData_ReturnErrorNotNull()
        {
            var core = CreateCore();
            var dataAll = GetDataAll(core);

            var res = await dataAll.Delete(ValidIdDelete);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task Delete_NoAuthValidData_ReturnErrorNotEmpty()
        {
            var core = CreateCore();
            var dataAll = GetDataAll(core);

            var res = await dataAll.Delete(ValidIdDelete);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Delete_NoAuthValidData_ReturnErrorType()
        {
            var core = CreateCore();
            var dataAll = GetDataAll(core);

            var res = await dataAll.Delete(ValidIdDelete);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task Delete_Zero_ReturnNotSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);

            var res = await dataAll.Delete(0);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Delete_Zero_ReturnErrorsNotNull()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);

            var res = await dataAll.Delete(0);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task Delete_Zero_ReturnErrorsType()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);

            var res = await dataAll.Delete(0);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task Delete_Zero_ReturnErrorsNotEmpty()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);

            var res = await dataAll.Delete(0);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnNotSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);

            var res = await dataAll.Delete(InvalidId);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Delete_ValidData_ReturnSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataAll = GetDataAll(core);

            var res = await dataAll.Delete(ValidIdDelete);

            Assert.True(res.Success);
        }
        #endregion
    }
}