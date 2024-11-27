using Deve.External;

namespace Deve.Tests
{
    /// <summary>
    /// DataGet methods tests.
    /// The ExecuteValidLogin is used to avoid permissions errors.
    /// </summary>
    public abstract class TestBaseDataGet<TDataType, ModelList, Model, Criteria> : TestBase<TDataType> where Model: ModelId where TDataType: IDataCommon
    {
        #region Properties
        protected virtual long ValidId => TestsConstants.DefaultValidId;
        protected virtual long InvalidId => TestsConstants.DefaultInvalidId;
        #endregion

        #region Abstract Methods
        protected abstract IDataGet<ModelList, Model, Criteria> GetDataGet(TDataType data);
        #endregion

        #region GetList
        [Fact]
        public async Task GetList_CriteriaNull_ReturnNotNull()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataGet = GetDataGet(data);

            var res = await dataGet.Get(default(Criteria?));

            Assert.NotNull(res);
        }

        [Fact]
        public async Task GetList_CriteriaNull_ReturnSuccess()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataGet = GetDataGet(data);

            var res = await dataGet.Get(default(Criteria?));

            Assert.True(res.Success);
        }

        [Fact]
        public async Task GetList_CriteriaNull_ReturnDataType()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataGet = GetDataGet(data);

            var res = await dataGet.Get(default(Criteria?));

            Assert.IsAssignableFrom<IList<ModelList>>(res.Data);
        }

        [Fact]
        public async Task GetList_CriteriaNull_ReturnNotEmpty()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataGet = GetDataGet(data);

            var res = await dataGet.Get(default(Criteria?));

            Assert.NotEmpty(res.Data);
        }

        [Fact]
        public async Task GetList_CriteriaNull_ReturnFirstItemNotNull()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataGet = GetDataGet(data);

            var res = await dataGet.Get(default(Criteria?));

            Assert.NotNull(res.Data.FirstOrDefault());
        }
        #endregion

        #region Get
        [Fact]
        public async Task Get_Zero_ReturnResultNotNull()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataGet = GetDataGet(data);

            var res = await dataGet.Get(0);

            Assert.NotNull(res);
        }

        [Fact]
        public async Task Get_Zero_ReturnNotSuccess()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataGet = GetDataGet(data);

            var res = await dataGet.Get(0);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Get_Zero_ReturnDataNull()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataGet = GetDataGet(data);

            var res = await dataGet.Get(0);

            Assert.Null(res.Data);
        }

        [Fact]
        public async Task Get_InvalidId_ReturnNotSuccess()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataGet = GetDataGet(data);

            var res = await dataGet.Get(InvalidId);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Get_ValidId_ReturnResultNotNull()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataGet = GetDataGet(data);

            var res = await dataGet.Get(ValidId);

            Assert.NotNull(res);
        }

        [Fact]
        public async Task Get_ValidId_ReturnSuccess()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataGet = GetDataGet(data);

            var res = await dataGet.Get(ValidId);

            Assert.True(res.Success);
        }

        [Fact]
        public async Task Get_ValidId_ReturnDataNotNull()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataGet = GetDataGet(data);

            var res = await dataGet.Get(ValidId);

            Assert.NotNull(res.Data);
        }

        [Fact]
        public async Task Get_ValidId_ReturnDataIdMatch()
        {
            var data = await CreateDataAndExecuteValidLogin();
            var dataGet = GetDataGet(data);

            var res = await dataGet.Get(ValidId);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.NotNull(res.Data);
            Assert.Equal(ValidId, res.Data.Id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        #endregion
    }
}