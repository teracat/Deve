namespace Deve.Tests
{
    /// <summary>
    /// Authenticate Tests.
    /// </summary>
    public abstract class TestAuthenticate<TDataType> : TestBase<TDataType> where TDataType: IDataCommon
    {
        [Fact]
        public async Task Login_CredentialsNull_ReturnFalse()
        {
            var data = CreateData();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await data.Authenticate.Login(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsDefConstructor_ReturnFalse()
        {
            var data = CreateData();

            var result = await data.Authenticate.Login(new UserCredentials());

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsEmpty_ReturnFalse()
        {
            var data = CreateData();

            var result = await data.Authenticate.Login(new UserCredentials(string.Empty, string.Empty));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsNotValid_ReturnFalse()
        {
            var data = CreateData();

            var result = await data.Authenticate.Login(new UserCredentials("aa", "bb"));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsInactive_ReturnFalse()
        {
            var data = CreateData();

            var result = await data.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameInactive, TestsConstants.UserPasswordInactive));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsValid_ReturnTrue()
        {
            var data = CreateData();

            var result = await data.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));

            Assert.True(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Null_ReturnFalse()
        {
            var data = CreateData();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await data.Authenticate.RefreshToken(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Empty_ReturnFalse()
        {
            var data = CreateData();

            var result = await data.Authenticate.RefreshToken(string.Empty);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_NotValid_ReturnFalse()
        {
            var data = CreateData();

            var result = await data.Authenticate.RefreshToken("aa");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Expired_ReturnFalse()
        {
            var data = CreateData();

            var result = await data.Authenticate.RefreshToken(TestsConstants.TokenExpired);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_InactiveUser_ReturnFalse()
        {
            var userToken = TestsHelpers.CreateTokenInactiveUser();
            var data = CreateData();

            var result = await data.Authenticate.RefreshToken(userToken.Token);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Valid_ReturnTrue()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var data = CreateData();

            var result = await data.Authenticate.RefreshToken(userToken.Token);

            Assert.True(result.Success);
        }
    }
}