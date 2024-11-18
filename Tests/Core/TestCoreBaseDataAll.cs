using Deve.Internal;

namespace Deve.Tests.Core
{
    /// <summary>
    /// DataAll methods tests.
    /// The ExecuteValidLogin is executed in every test to avoid permissions errors.
    /// </summary>
    public abstract class TestCoreBaseDataAll<ModelList, Model, Criteria> : TestCoreBase where Model: ModelId
    {
        protected abstract IDataAll<ModelList, Model, Criteria> DataAll { get; }

        protected virtual long ValidId => 1;
        protected virtual long InvalidId => 999999;

        protected abstract Model CreateInvalidDataToAdd();
        protected abstract Model CreateValidDataToAdd();

        #region GetList
        [Fact]
        public async Task GetList_CriteriaNull_ReturnNotNull()
        {
            await ExecuteValidLogin();

            var res = await DataAll.Get(default(Criteria?));

            Assert.NotNull(res);
        }

        [Fact]
        public async Task GetList_CriteriaNull_ReturnSuccess()
        {
            await ExecuteValidLogin();

            var res = await DataAll.Get(default(Criteria?));

            Assert.True(res.Success);
        }

        [Fact]
        public async Task GetList_CriteriaNull_ReturnDataType()
        {
            await ExecuteValidLogin();

            var res = await DataAll.Get(default(Criteria?));

            Assert.IsAssignableFrom<IList<Country>>(res.Data);
        }

        [Fact]
        public async Task GetList_CriteriaNull_ReturnNotEmpty()
        {
            await ExecuteValidLogin();

            var res = await DataAll.Get(default(Criteria?));

            Assert.NotEmpty(res.Data);
        }

        [Fact]
        public async Task GetList_CriteriaNull_ReturnFirstItemNotNull()
        {
            await ExecuteValidLogin();

            var res = await DataAll.Get(default(Criteria?));

            Assert.NotNull(res.Data.FirstOrDefault());
        }
        #endregion

        #region Get
        [Fact]
        public async Task Get_Zero_ReturnResultNotNull()
        {
            await ExecuteValidLogin();

            var res = await DataAll.Get(0);

            Assert.NotNull(res);
        }

        [Fact]
        public async Task Get_Zero_ReturnNotSuccess()
        {
            await ExecuteValidLogin();

            var res = await DataAll.Get(0);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Get_Zero_ReturnDataNull()
        {
            await ExecuteValidLogin();

            var res = await DataAll.Get(0);

            Assert.Null(res.Data);
        }

        [Fact]
        public async Task Get_InvalidId_ReturnNotSuccess()
        {
            await ExecuteValidLogin();

            var res = await DataAll.Get(InvalidId);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Get_ValidId_ReturnResultNotNull()
        {
            await ExecuteValidLogin();

            var res = await DataAll.Get(ValidId);

            Assert.NotNull(res);
        }

        [Fact]
        public async Task Get_ValidId_ReturnSuccess()
        {
            await ExecuteValidLogin();

            var res = await DataAll.Get(ValidId);

            Assert.True(res.Success);
        }

        [Fact]
        public async Task Get_ValidId_ReturnDataNotNull()
        {
            await ExecuteValidLogin();

            var res = await DataAll.Get(ValidId);

            Assert.NotNull(res.Data);
        }

        [Fact]
        public async Task Get_ValidId_ReturnDataIdMatch()
        {
            await ExecuteValidLogin();

            var res = await DataAll.Get(ValidId);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.Equal(ValidId, res.Data.Id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        #endregion

        #region Add
        [Fact]
        public async Task Add_Null_ReturnNotSuccess()
        {
            await ExecuteValidLogin();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var res = await DataAll.Add(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Add_InvalidData_ReturnNotSuccess()
        {
            await ExecuteValidLogin();
            var data = CreateInvalidDataToAdd();

            var res = await DataAll.Add(data);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Add_InvalidData_ReturnErrorsNotNull()
        {
            await ExecuteValidLogin();
            var data = CreateInvalidDataToAdd();

            var res = await DataAll.Add(data);

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task Add_InvalidData_ReturnErrorsType()
        {
            await ExecuteValidLogin();
            var data = CreateInvalidDataToAdd();

            var res = await DataAll.Add(data);

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task Add_InvalidData_ReturnErrorsNotEmpty()
        {
            await ExecuteValidLogin();
            var data = CreateInvalidDataToAdd();

            var res = await DataAll.Add(data);

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task Add_InvalidData_ReturnFirstErrorNotNull()
        {
            await ExecuteValidLogin();
            var data = CreateInvalidDataToAdd();

            var res = await DataAll.Add(data);

            Assert.NotNull(res.Errors.FirstOrDefault());
        }

        [Fact]
        public async Task Add_ValidData_ReturnSuccess()
        {
            await ExecuteValidLogin();
            var data = CreateValidDataToAdd();

            var res = await DataAll.Add(data);

            Assert.True(res.Success);
        }
        #endregion
    }
}