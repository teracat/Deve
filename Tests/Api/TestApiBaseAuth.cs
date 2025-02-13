using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api
{
    /// <summary>
    /// Api Auth endpoints tests.
    /// The Auth methods are defined in the Common.Api, so both the External and Internal Api will have the same methods.
    /// Here we define the common methods to test in both Apis, but the tests are made in every class to implement the Program class of every Api.
    /// </summary>
    public abstract class TestApiBaseAuth<TEntryPoint> : TestApiBase<TEntryPoint> where TEntryPoint : class
    {
        #region Constructor
        protected TestApiBaseAuth(FixtureApiClients<TEntryPoint> fixture)
            : base(fixture)
        {
        }
        #endregion

        #region Common
        [Theory]
        [InlineData(ApiConstants.PathAuth + ApiConstants.MethodLogin)]
        [InlineData(ApiConstants.PathAuth + ApiConstants.MethodRefreshToken)]
        public async Task Get_Empty_NotSuccessStatusCode(string path)
        {
            var response = await Fixture.ClientNoAuth.GetAsync(path);

            Assert.False(response.IsSuccessStatusCode);
        }
        #endregion

        #region Login
        [Fact]
        public async Task Login_EmptyCredentials_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientNoAuth.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodLogin + "?username=&password=");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Login_NotValidCredentials_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientNoAuth.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodLogin + "?username=aa&password=aa");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Login_InactiveUserCredentials_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientNoAuth.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodLogin + $"?username={TestsConstants.UserUsernameInactive}&password={TestsConstants.UserPasswordInactive}");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task Login_ValidCredentials_SuccessStatusCode()
        {
            var response = await Fixture.ClientNoAuth.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodLogin + $"?username={TestsConstants.UserUsernameValid}&password={TestsConstants.UserPasswordValid}");

            Assert.True(response.IsSuccessStatusCode);
        }
        #endregion

        #region RefreshToken
        [Fact]
        public async Task RefreshToken_EmptyToken_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientNoAuth.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodRefreshToken + "?token=");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task RefreshToken_NotValidToken_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientNoAuth.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodRefreshToken + "?token=aa");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task RefreshToken_ExpiredToken_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientNoAuth.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodRefreshToken + $"?token={TestsConstants.TokenExpired}");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task RefreshToken_InactiveUser_NotSuccessStatusCode()
        {
            var response = await Fixture.ClientNoAuth.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodRefreshToken + $"?token={Fixture.UserTokenInactiveUser.Token}");

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task RefreshToken_Valid_SuccessStatusCode()
        {
            var response = await Fixture.ClientNoAuth.GetAsync(ApiConstants.PathAuth + ApiConstants.MethodRefreshToken + $"?token={Fixture.UserTokenValid.Token}");

            Assert.True(response.IsSuccessStatusCode);
        }
        #endregion
    }
}