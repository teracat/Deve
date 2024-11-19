using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreStats : TestCoreBase
    {
        [Fact]
        public async Task GetClientStats_NoAuth_ReturnNotSuccess()
        {
            var core = CreateCore();

            var res = await core.Stats.GetClientStats();

            Assert.False(res.Success);
        }

        [Fact]
        public async Task GetClientStats_NoAuth_ReturnErrorNotNull()
        {
            var core = CreateCore();

            var res = await core.Stats.GetClientStats();

            Assert.NotNull(res.Errors);
        }

        [Fact]
        public async Task GetClientStats_NoAuth_ReturnErrorNotEmpty()
        {
            var core = CreateCore();

            var res = await core.Stats.GetClientStats();

            Assert.NotEmpty(res.Errors);
        }

        [Fact]
        public async Task GetClientStats_NoAuth_ReturnErrorType()
        {
            var core = CreateCore();

            var res = await core.Stats.GetClientStats();

            Assert.IsAssignableFrom<IList<ResultError>>(res.Errors);
        }



        [Fact]
        public async Task GetClientStats_Auth_ReturnSuccess()
        {
            var core = await CreateCoreAndExecuteValidLogin();

            var res = await core.Stats.GetClientStats();

            Assert.True(res.Success);
        }

        [Fact]
        public async Task GetClientStats_Auth_ReturnDataNotNull()
        {
            var core = await CreateCoreAndExecuteValidLogin();

            var res = await core.Stats.GetClientStats();

            Assert.NotNull(res.Data);
        }

        [Fact]
        public async Task GetClientStats_Auth_ReturnDataType()
        {
            var core = await CreateCoreAndExecuteValidLogin();

            var res = await core.Stats.GetClientStats();

            Assert.IsAssignableFrom<ClientStats>(res.Data);
        }
    }
}