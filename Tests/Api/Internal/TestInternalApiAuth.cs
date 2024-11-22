using Microsoft.AspNetCore.Mvc.Testing;
using Deve.Internal.Api;

namespace Deve.Tests.Api.Internal
{
    /// <summary>
    /// Internal Api Auth endpoints tests.
    /// </summary>
    public class TestInternalApiAuth : TestInternalApiBase
    {
        public TestInternalApiAuth(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Theory]
        [InlineData(ApiConstants.PathAuth + ApiConstants.MethodLogin)]
        [InlineData(ApiConstants.PathAuth + ApiConstants.MethodRefreshToken)]
        public async Task Get_Empty_NotSuccessStatusCode(string path)
        {
            var client = CreateClient();

            var response = await client.GetAsync(path);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Login_EmptyCredentials_NotSuccessStatusCode()
        {
            var client = CreateClient();

            var response = await client.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodLogin + $"?username=&password=");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Login_NotValidCredentials_NotSuccessStatusCode()
        {
            var client = CreateClient();

            var response = await client.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodLogin + $"?username=aa&password=aa");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Login_ValidCredentials_SuccessStatusCode()
        {
            var client = CreateClient();

            var response = await client.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodLogin + $"?username={TestsHelpers.ValidUsername}&password={TestsHelpers.ValidPassword}");

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task RefreshToken_EmptyToken_NotSuccessStatusCode()
        {
            var client = CreateClient();

            var response = await client.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodRefreshToken + $"?token=");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task RefreshToken_NotValidToken_NotSuccessStatusCode()
        {
            var client = CreateClient();

            var response = await client.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodRefreshToken + $"?token=aa");

            Assert.False(response.IsSuccessStatusCode);
        }
    }
}