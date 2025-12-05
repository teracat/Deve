using Deve.Authenticate;
using Deve.Data;

namespace Deve.Tests
{
    /// <summary>
    /// Authenticate Tests.
    /// </summary>
    public abstract class TestAuthenticate<TDataType> : TestBase<TDataType> where TDataType : IDataCommon
    {
        protected TestAuthenticate(IFixtureData<TDataType> fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task Login_CredentialsNull_ReturnFalse()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await Fixture.DataNoAuth.Authenticate.Login(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsDefConstructor_ReturnFalse()
        {
            var result = await Fixture.DataNoAuth.Authenticate.Login(new UserCredentials());

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsEmpty_ReturnFalse()
        {
            var result = await Fixture.DataNoAuth.Authenticate.Login(new UserCredentials(string.Empty, string.Empty));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsNotValid_ReturnFalse()
        {
            var result = await Fixture.DataNoAuth.Authenticate.Login(new UserCredentials("aa", "bb"));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsInactive_ReturnFalse()
        {
            var result = await Fixture.DataNoAuth.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameInactive, TestsConstants.UserPasswordInactive));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsValid_ReturnTrue()
        {
            var result = await Fixture.DataNoAuth.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));

            Assert.True(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Null_ReturnFalse()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await Fixture.DataNoAuth.Authenticate.RefreshToken(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Empty_ReturnFalse()
        {
            var result = await Fixture.DataNoAuth.Authenticate.RefreshToken(string.Empty);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_NotValid_ReturnFalse()
        {
            var result = await Fixture.DataNoAuth.Authenticate.RefreshToken("aa");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Expired_ReturnFalse()
        {
            var result = await Fixture.DataNoAuth.Authenticate.RefreshToken(TestsConstants.TokenExpired);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_InactiveUser_ReturnFalse()
        {
            var userToken = TestsHelpers.CreateTokenInactiveUser(Fixture.TokenManager);

            var result = await Fixture.DataNoAuth.Authenticate.RefreshToken(userToken.Token);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Valid_ReturnTrue()
        {
            var userToken = TestsHelpers.CreateTokenValid(Fixture.TokenManager);

            var result = await Fixture.DataNoAuth.Authenticate.RefreshToken(userToken.Token);

            Assert.True(result.Success);
        }
    }
}