using Deve.Model;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api
{
    public abstract class TestApiBaseAll<TEntryPoint, Model> : TestApiBaseGet<TEntryPoint> where TEntryPoint : class where Model: ModelId
    {
        #region Properties
        protected virtual long ValidIdDelete => TestsConstants.DefaultValidIdDelete;
        #endregion

        #region Constructor
        public TestApiBaseAll(FixtureApiClients<TEntryPoint> fixture)
            : base(fixture)
        {
        }
        #endregion

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
            var response = await Fixture.ClientNoAuth.PostAsync(Path, null);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Add_NullRequestBody_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientValidAuth.PostAsync(Path, null);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Add_InvalidData_NotSuccessStatusCode()
        {
            var data = CreateInvalidDataToAdd();

            var response = await Fixture.ClientValidAuth.PostAsync(Path, ToHttpContent(data));

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Add_ValidData_SuccessStatusCode()
        {
            var data = CreateValidDataToAdd();

            var response = await Fixture.ClientValidAuth.PostAsync(Path, ToHttpContent(data));

            Assert.True(response.IsSuccessStatusCode);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Update_Unauthorized_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientNoAuth.PutAsync(Path, null);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Update_NullRequestBody_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientValidAuth.PutAsync(Path, null);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Update_InvalidData_NotSuccessStatusCode()
        {
            var data = CreateInvalidDataToUpdate();

            var response = await Fixture.ClientValidAuth.PutAsync(Path, ToHttpContent(data));

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Update_ValidData_SuccessStatusCode()
        {
            var data = CreateValidDataToUpdate();

            var response = await Fixture.ClientValidAuth.PutAsync(Path, ToHttpContent(data));

            Assert.True(response.IsSuccessStatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public async Task Delete_Unauthorized_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientNoAuth.DeleteAsync(Path + "0");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Delete_ZeroId_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientValidAuth.DeleteAsync(Path + "0");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Delete_InvalidId_NotSuccessStatusCode()
        {
            var data = CreateInvalidDataToUpdate();

            var response = await Fixture.ClientValidAuth.DeleteAsync(Path + InvalidId);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Delete_ValidId_SuccessStatusCode()
        {
            var data = CreateValidDataToUpdate();

            var response = await Fixture.ClientValidAuth.DeleteAsync(Path + ValidIdDelete);

            Assert.True(response.IsSuccessStatusCode);
        }
        #endregion
    }
}