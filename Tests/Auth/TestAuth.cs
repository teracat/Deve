using Deve.Auth.Permissions;
using Deve.Authenticate;
using Deve.Tests.Auth.Fixtures;

namespace Deve.Tests.Auth
{
    /// <summary>
    /// Auth Tests.
    /// </summary>
    public class TestAuth : IClassFixture<FixtureAuth>
    {
        private readonly FixtureAuth _fixtureAuth;

        public TestAuth(FixtureAuth authFixture)
        {
            _fixtureAuth = authFixture;
        }

        #region Login
        [Fact]
        public async Task Login_CredentialsNull_ReturnFalse()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await _fixtureAuth.Auth.Login(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsDefConstructor_ReturnFalse()
        {
            var result = await _fixtureAuth.Auth.Login(new UserCredentials());

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsEmpty_ReturnFalse()
        {
            var result = await _fixtureAuth.Auth.Login(new UserCredentials(string.Empty, string.Empty));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsNotValid_ReturnFalse()
        {
            var result = await _fixtureAuth.Auth.Login(new UserCredentials("aa", "bb"));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsInactiveUser_ReturnFalse()
        {
            var result = await _fixtureAuth.Auth.Login(new UserCredentials(TestsConstants.UserUsernameInactive, TestsConstants.UserPasswordInactive));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsValid_ReturnTrue()
        {
            var result = await _fixtureAuth.Auth.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));

            Assert.True(result.Success);
        }
        #endregion

        #region RefershToken
        [Fact]
        public async Task RefreshToken_Null_ReturnFalse()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await _fixtureAuth.Auth.RefreshToken(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Empty_ReturnFalse()
        {
            var result = await _fixtureAuth.Auth.RefreshToken(string.Empty);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_NotValid_ReturnFalse()
        {
            var result = await _fixtureAuth.Auth.RefreshToken("aa");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Expired_ReturnFalse()
        {
            var result = await _fixtureAuth.Auth.RefreshToken(TestsConstants.TokenExpired);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_InactiveUser_ReturnFalse()
        {
            var result = await _fixtureAuth.Auth.RefreshToken(_fixtureAuth.UserTokenInactive.Token);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Valid_ReturnTrue()
        {
            var result = await _fixtureAuth.Auth.RefreshToken(_fixtureAuth.UserTokenValid.Token);

            Assert.True(result.Success);
        }
        #endregion

        #region IsGranted
        [Fact]
        public async Task IsGranted_NullGetListState_ReturnGranted()
        {
            var result = await _fixtureAuth.Auth.IsGranted(null, PermissionType.GetList, PermissionDataType.State);

            Assert.Equal(PermissionResult.Granted, result);
        }

        [Fact]
        public async Task IsGranted_UserGetListState_ReturnGranted()
        {
            var result = await _fixtureAuth.Auth.IsGranted(_fixtureAuth.UserIdentityUser, PermissionType.GetList, PermissionDataType.State);

            Assert.Equal(PermissionResult.Granted, result);
        }

        [Fact]
        public async Task IsGranted_AdminGetListState_ReturnGranted()
        {
            var result = await _fixtureAuth.Auth.IsGranted(_fixtureAuth.UserIdentityAdmin, PermissionType.GetList, PermissionDataType.State);

            Assert.Equal(PermissionResult.Granted, result);
        }

        [Fact]
        public async Task IsGranted_UserAddState_ReturnNotGranted()
        {
            var result = await _fixtureAuth.Auth.IsGranted(_fixtureAuth.UserIdentityUser, PermissionType.Add, PermissionDataType.State);

            Assert.Equal(PermissionResult.NotGranted, result);
        }

        [Fact]
        public async Task IsGranted_AdminAddState_ReturnGranted()
        {
            var result = await _fixtureAuth.Auth.IsGranted(_fixtureAuth.UserIdentityAdmin, PermissionType.Add, PermissionDataType.State);

            Assert.Equal(PermissionResult.Granted, result);
        }
        #endregion
    }
}
