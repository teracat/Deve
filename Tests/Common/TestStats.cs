using Deve.Internal;

namespace Deve.Tests
{
    public abstract class TestStats<TDataType> : TestBase<TDataType> where TDataType : IData
    {
        #region Constructor
        public TestStats(IFixtureData<TDataType> fixtureData, IFixtureDataLogged<TDataType> fixtureDataLogged)
            : base(fixtureData, fixtureDataLogged)
        {
        }
        #endregion

        #region Tests
        [Fact]
        public async Task GetClientStats_NoAuth_ReturnNotSuccess()
        {
            var res = await FixtureData.Data.Stats.GetClientStats();

            Assert.False(res.Success);
        }

        [Fact]
        public async Task GetClientStats_NoAuth_ReturnErrorNotNull()
        {
            var res = await FixtureData.Data.Stats.GetClientStats();

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task GetClientStats_NoAuth_ReturnErrorNotEmpty()
        {
            var res = await FixtureData.Data.Stats.GetClientStats();

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task GetClientStats_NoAuth_ReturnErrorType()
        {
            var res = await FixtureData.Data.Stats.GetClientStats();

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task GetClientStats_Auth_ReturnSuccess()
        {
            var res = await FixtureDataLogged.Data.Stats.GetClientStats();

            Assert.True(res.Success);
        }

        [Fact]
        public async Task GetClientStats_Auth_ReturnDataNotNull()
        {
            var res = await FixtureDataLogged.Data.Stats.GetClientStats();

            Assert.NotNull(res.Data);
        }

        [Fact]
        public async Task GetClientStats_Auth_ReturnDataType()
        {
            var res = await FixtureDataLogged.Data.Stats.GetClientStats();

            Assert.IsAssignableFrom<ClientStats>(res.Data);
        }
        #endregion
    }
}