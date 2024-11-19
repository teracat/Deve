using Deve.Core;
using Deve.External;

namespace Deve.Tests.Core
{
    /// <summary>
    /// DataGet methods tests.
    /// The ExecuteValidLogin is used to avoid permissions errors.
    /// </summary>
    public abstract class TestCoreBaseDataGet<ModelList, Model, Criteria> : TestCoreBase where Model: ModelId
    {
        #region Properties
        protected virtual long ValidId => 1;
        protected virtual long InvalidId => 999999;
        #endregion

        #region Abstract Methods
        protected abstract IDataGet<ModelList, Model, Criteria> GetDataGet(ICore core);
        #endregion

        #region GetList
        [Fact]
        public async Task GetList_CriteriaNull_ReturnNotNull()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataGet = GetDataGet(core);

            var res = await dataGet.Get(default(Criteria?));

            Assert.NotNull(res);
        }

        [Fact]
        public async Task GetList_CriteriaNull_ReturnSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataGet = GetDataGet(core);

            var res = await dataGet.Get(default(Criteria?));

            Assert.True(res.Success);
        }

        [Fact]
        public async Task GetList_CriteriaNull_ReturnDataType()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataGet = GetDataGet(core);

            var res = await dataGet.Get(default(Criteria?));

            Assert.IsAssignableFrom<IList<ModelList>>(res.Data);
        }

        [Fact]
        public async Task GetList_CriteriaNull_ReturnNotEmpty()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataGet = GetDataGet(core);

            var res = await dataGet.Get(default(Criteria?));

            Assert.NotEmpty(res.Data);
        }

        [Fact]
        public async Task GetList_CriteriaNull_ReturnFirstItemNotNull()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataGet = GetDataGet(core);

            var res = await dataGet.Get(default(Criteria?));

            Assert.NotNull(res.Data.FirstOrDefault());
        }
        #endregion

        #region Get
        [Fact]
        public async Task Get_Zero_ReturnResultNotNull()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataGet = GetDataGet(core);

            var res = await dataGet.Get(0);

            Assert.NotNull(res);
        }

        [Fact]
        public async Task Get_Zero_ReturnNotSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataGet = GetDataGet(core);

            var res = await dataGet.Get(0);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Get_Zero_ReturnDataNull()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataGet = GetDataGet(core);

            var res = await dataGet.Get(0);

            Assert.Null(res.Data);
        }

        [Fact]
        public async Task Get_InvalidId_ReturnNotSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataGet = GetDataGet(core);

            var res = await dataGet.Get(InvalidId);

            Assert.False(res.Success);
        }

        [Fact]
        public async Task Get_ValidId_ReturnResultNotNull()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataGet = GetDataGet(core);

            var res = await dataGet.Get(ValidId);

            Assert.NotNull(res);
        }

        [Fact]
        public async Task Get_ValidId_ReturnSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataGet = GetDataGet(core);

            var res = await dataGet.Get(ValidId);

            Assert.True(res.Success);
        }

        [Fact]
        public async Task Get_ValidId_ReturnDataNotNull()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataGet = GetDataGet(core);

            var res = await dataGet.Get(ValidId);

            Assert.NotNull(res.Data);
        }

        [Fact]
        public async Task Get_ValidId_ReturnDataIdMatch()
        {
            var core = await CreateCoreAndExecuteValidLogin();
            var dataGet = GetDataGet(core);

            var res = await dataGet.Get(ValidId);

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.Equal(ValidId, res.Data.Id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        #endregion
    }
}