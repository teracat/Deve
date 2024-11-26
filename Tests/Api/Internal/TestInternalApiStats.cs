using Microsoft.AspNetCore.Mvc.Testing;
using Deve.Internal.Api;
using Deve.Internal;

namespace Deve.Tests.Api.Internal
{
    public class TestInternalApiStats : TestApiBase<Program>
    {
        public TestInternalApiStats(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task GetClientStats_Unauthorized_NotSuccessStatusCode()
        {
            var client = CreateClient();

            var response = await client.GetAsync(ApiConstants.PathStats + ApiConstants.MethodGetClientStats + $"0/{(int)ClientStatus.Inactive}");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetClientStats_Valid_SuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);

            var response = await client.GetAsync(ApiConstants.PathStats + ApiConstants.MethodGetClientStats);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}