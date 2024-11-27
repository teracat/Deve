using Deve.Internal;

namespace Deve.Tests
{
    public abstract class TestStats : TestBase<IData>
    {
        [Fact]
        public async Task GetClientStats_NoAuth_ReturnNotSuccess()
        {
            var data = CreateData();

            var res = await data.Stats.GetClientStats();

            Assert.False(res.Success);
        }

        [Fact]
        public async Task GetClientStats_NoAuth_ReturnErrorNotNull()
        {
            var data = CreateData();

            var res = await data.Stats.GetClientStats();

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task GetClientStats_NoAuth_ReturnErrorNotEmpty()
        {
            var data = CreateData();

            var res = await data.Stats.GetClientStats();

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task GetClientStats_NoAuth_ReturnErrorType()
        {
            var data = CreateData();

            var res = await data.Stats.GetClientStats();

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }

        [Fact]
        public async Task GetClientStats_Auth_ReturnSuccess()
        {
            var data = await CreateDataAndExecuteValidLogin();

            var res = await data.Stats.GetClientStats();

            Assert.True(res.Success);
        }

        [Fact]
        public async Task GetClientStats_Auth_ReturnDataNotNull()
        {
            var data = await CreateDataAndExecuteValidLogin();

            var res = await data.Stats.GetClientStats();

            Assert.NotNull(res.Data);
        }

        [Fact]
        public async Task GetClientStats_Auth_ReturnDataType()
        {
            var data = await CreateDataAndExecuteValidLogin();

            var res = await data.Stats.GetClientStats();

            Assert.IsAssignableFrom<ClientStats>(res.Data);
        }
    }
}