using Microsoft.AspNetCore.Mvc.Testing;

namespace Deve.Tests.Api
{
    public abstract class TestApiBaseAll<TEntryPoint, Model> : TestApiBaseGet<TEntryPoint> where TEntryPoint : class where Model: ModelId
    {
        protected virtual long ValidIdDelete => TestsConstants.DefaultValidIdDelete;

        public TestApiBaseAll(WebApplicationFactory<TEntryPoint> factory)
            : base(factory)
        {
        }

        #region Abstract Methods
        protected abstract Model CreateInvalidDataToAdd();
        protected abstract Model CreateInvalidDataToUpdate();
        protected abstract Model CreateValidDataToAdd();
        protected abstract Model CreateValidDataToUpdate();
        #endregion

        #region Add
        [Fact]
        public async Task Add_Unauthorized_NotSuccessStatusCode()
        {
            var client = CreateClient();

            var response = await client.PostAsync(Path, null);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Add_NullRequestBody_NotSuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);

            var response = await client.PostAsync(Path, null);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Add_InvalidData_NotSuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);
            var data = CreateInvalidDataToAdd();

            var response = await client.PostAsync(Path, ToHttpContent(data));

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Add_ValidData_SuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);
            var data = CreateValidDataToAdd();

            var response = await client.PostAsync(Path, ToHttpContent(data));

            Assert.True(response.IsSuccessStatusCode);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_Unauthorized_NotSuccessStatusCode()
        {
            var client = CreateClient();

            var response = await client.PutAsync(Path, null);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Update_NullRequestBody_NotSuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);

            var response = await client.PutAsync(Path, null);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Update_InvalidData_NotSuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);
            var data = CreateInvalidDataToUpdate();

            var response = await client.PutAsync(Path, ToHttpContent(data));

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Update_ValidData_SuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);
            var data = CreateValidDataToUpdate();

            var response = await client.PutAsync(Path, ToHttpContent(data));

            Assert.True(response.IsSuccessStatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_Unauthorized_NotSuccessStatusCode()
        {
            var client = CreateClient();

            var response = await client.DeleteAsync(Path + "0");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Delete_ZeroId_NotSuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);

            var response = await client.DeleteAsync(Path + "0");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Delete_InvalidId_NotSuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);
            var data = CreateInvalidDataToUpdate();

            var response = await client.DeleteAsync(Path + InvalidId);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Delete_ValidId_SuccessStatusCode()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var client = CreateClientWithAuth(userToken.Token);
            var data = CreateValidDataToUpdate();

            var response = await client.DeleteAsync(Path + ValidIdDelete);

            Assert.True(response.IsSuccessStatusCode);
        }
        #endregion
    }
}