using Deve.Internal.Api;
using Deve.Internal;

namespace Deve.Tests.Api.Internal
{
    public class TestInternalApiStats : TestApiBase<Program>, IClassFixture<FixtureApiInternal>
    {
        public TestInternalApiStats(FixtureApiInternal fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task GetClientStats_Unauthorized_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientNoAuth.GetAsync(ApiConstants.PathStats + ApiConstants.MethodGetClientStats + $"0/{(int)ClientStatus.Inactive}");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetClientStats_Valid_SuccessStatusCode()
        {
            var response = await Fixture.ClientValidAuth.GetAsync(ApiConstants.PathStats + ApiConstants.MethodGetClientStats);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}