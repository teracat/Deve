using Deve.Data;
using Deve.Model;
using Deve.External.Data;
using Deve.Internal.Data;

namespace Deve.Tests
{
    /// <summary>
    /// DataAll methods tests.
    /// The ExecuteValidLogin is used to avoid permissions errors.
    /// </summary>
    public abstract class TestBaseDataAll<TDataType, ModelList, Model, Criteria> : TestBaseDataGet<TDataType, ModelList, Model, Criteria> where Model: ModelId where TDataType : IDataCommon
    {
        #region Abstract Methods
        protected abstract IDataAll<ModelList, Model, Criteria> GetDataAll(TDataType data);

        protected abstract Model CreateInvalidDataToAdd();
        protected abstract Model CreateInvalidDataToUpdate();
        protected abstract Model CreateValidDataToAdd();
        protected abstract Model CreateValidDataToUpdate();
        #endregion

        #region Properties
        /// <summary>
        /// Id used in the Delete methods tests as a valid Id to be deleted.
        /// Should be different from the ValidId defined in TestBaseDataGet and different from the Id used for Update methods.
        /// This Id should not be used in the data used to Add or Update in other tests.
        /// </summary>
        protected virtual long ValidIdDelete => TestsConstants.DefaultValidIdDelete;
        #endregion

        #region Constructor
        protected TestBaseDataAll(IFixtureData<TDataType> fixture)
            : base(fixture)
        {
        }
        #endregion

        #region Override Methods
        protected override IDataGet<ModelList, Model, Criteria> GetDataGet(TDataType data) => GetDataAll(data);
        #endregion

        #region Add
        [Fact]
        public async Task Add_NoAuthValidData_ReturnNotSuccess()
        {
            var dataAll = GetDataAll(Fixture.DataNoAuth);
            var obj = CreateValidDataToAdd();

            var res = await dataAll.Add(obj);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Add_NoAuthValidData_ReturnErrorNotNull()
        {
            var dataAll = GetDataAll(Fixture.DataNoAuth);
            var obj = CreateValidDataToAdd();

            var res = await dataAll.Add(obj);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task Add_NoAuthValidData_ReturnErrorNotEmpty()
        {
            var dataAll = GetDataAll(Fixture.DataNoAuth);
            var obj = CreateValidDataToAdd();

            var res = await dataAll.Add(obj);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Add_NoAuthValidData_ReturnErrorType()
        {
            var dataAll = GetDataAll(Fixture.DataNoAuth);
            var obj = CreateValidDataToAdd();

            var res = await dataAll.Add(obj);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task Add_Null_ReturnNotSuccess()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var res = await dataAll.Add(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Add_InvalidData_ReturnNotSuccess()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);
            var obj = CreateInvalidDataToAdd();

            var res = await dataAll.Add(obj);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Add_InvalidData_ReturnErrorsNotNull()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);
            var obj = CreateInvalidDataToAdd();

            var res = await dataAll.Add(obj);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task Add_InvalidData_ReturnErrorsType()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);
            var obj = CreateInvalidDataToAdd();

            var res = await dataAll.Add(obj);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task Add_InvalidData_ReturnErrorsNotEmpty()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);
            var obj = CreateInvalidDataToAdd();

            var res = await dataAll.Add(obj);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Add_ValidData_ReturnSuccess()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);
            var obj = CreateValidDataToAdd();

            var res = await dataAll.Add(obj);

            Assert.True(res.Success, res.Errors?.FirstOrDefault()?.Description);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_NoAuthValidData_ReturnNotSuccess()
        {
            var dataAll = GetDataAll(Fixture.DataNoAuth);
            var obj = CreateValidDataToUpdate();

            var res = await dataAll.Update(obj);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Update_NoAuthValidData_ReturnErrorNotNull()
        {
            var dataAll = GetDataAll(Fixture.DataNoAuth);
            var obj = CreateValidDataToUpdate();

            var res = await dataAll.Update(obj);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task Update_NoAuthValidData_ReturnErrorNotEmpty()
        {
            var dataAll = GetDataAll(Fixture.DataNoAuth);
            var obj = CreateValidDataToUpdate();

            var res = await dataAll.Update(obj);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Update_NoAuthValidData_ReturnErrorType()
        {
            var dataAll = GetDataAll(Fixture.DataNoAuth);
            var obj = CreateValidDataToUpdate();

            var res = await dataAll.Update(obj);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task Update_Null_ReturnNotSuccess()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var res = await dataAll.Update(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Update_InvalidData_ReturnNotSuccess()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);
            var obj = CreateInvalidDataToUpdate();

            var res = await dataAll.Update(obj);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Update_InvalidData_ReturnErrorsNotNull()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);
            var obj = CreateInvalidDataToUpdate();

            var res = await dataAll.Update(obj);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task Update_InvalidData_ReturnErrorsType()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);
            var obj = CreateInvalidDataToUpdate();

            var res = await dataAll.Update(obj);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task Update_InvalidData_ReturnErrorsNotEmpty()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);
            var obj = CreateInvalidDataToUpdate();

            var res = await dataAll.Update(obj);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Update_ValidData_ReturnSuccess()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);
            var obj = CreateValidDataToUpdate();

            var res = await dataAll.Update(obj);

            Assert.True(res.Success, res.Errors?.FirstOrDefault()?.Description);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_NoAuthValidData_ReturnNotSuccess()
        {
            var dataAll = GetDataAll(Fixture.DataNoAuth);

            var res = await dataAll.Delete(ValidIdDelete);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Delete_NoAuthValidData_ReturnErrorNotNull()
        {
            var dataAll = GetDataAll(Fixture.DataNoAuth);

            var res = await dataAll.Delete(ValidIdDelete);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task Delete_NoAuthValidData_ReturnErrorNotEmpty()
        {
            var dataAll = GetDataAll(Fixture.DataNoAuth);

            var res = await dataAll.Delete(ValidIdDelete);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Delete_NoAuthValidData_ReturnErrorType()
        {
            var dataAll = GetDataAll(Fixture.DataNoAuth);

            var res = await dataAll.Delete(ValidIdDelete);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task Delete_Zero_ReturnNotSuccess()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);

            var res = await dataAll.Delete(0);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Delete_Zero_ReturnErrorsNotNull()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);

            var res = await dataAll.Delete(0);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task Delete_Zero_ReturnErrorsType()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);

            var res = await dataAll.Delete(0);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task Delete_Zero_ReturnErrorsNotEmpty()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);

            var res = await dataAll.Delete(0);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Delete_InvalidId_ReturnNotSuccess()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);

            var res = await dataAll.Delete(InvalidId);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Delete_ValidData_ReturnSuccess()
        {
            var dataAll = GetDataAll(Fixture.DataValidAuth);

            var res = await dataAll.Delete(ValidIdDelete);

            Assert.True(res.Success);
        }
        #endregion
    }
}