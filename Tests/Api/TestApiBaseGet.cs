using Microsoft.AspNetCore.Mvc.Testing;

namespace Deve.Tests.Api
{
    public abstract class TestApiBaseGet<TEntryPoint> : TestApiBase<TEntryPoint> where TEntryPoint : class
    {
        protected abstract string Path { get; }

        protected virtual string ValidCriteriaParameterName => "name";
        protected virtual long ValidId => 1;

        public TestApiBaseGet(WebApplicationFactory<TEntryPoint> factory)
            : base(factory)
        {
        }

        #region GetList
        [Fact]
        public async Task GetList_EmptyCriteria_SuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);

            var response = await client.GetAsync(Path);

            Assert.True(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task GetList_Parameter_SuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);

            var response = await client.GetAsync(Path + $"?{ValidCriteriaParameterName}=aa");

            Assert.True(response.IsSuccessStatusCode);
        }
        #endregion

        #region Get
        [Fact]
        public async Task Get_Zero_NotSuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);

            var response = await client.GetAsync(Path + "0");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Get_ValidId_SuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);

            var response = await client.GetAsync(Path + ValidId);

            Assert.True(response.IsSuccessStatusCode);
        }
        #endregion
    }
}