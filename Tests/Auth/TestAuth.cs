using Deve.Auth;

namespace Deve.Tests.Auth
{
    /// <summary>
    /// Auth Tests.
    /// </summary>
    public class TestAuth
    {
        #region Login
        [Fact]
        public async Task Login_CredentialsNull_ReturnFalse()
        {
            var auth = TestsHelpers.CreateAuth();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await auth.Login(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsDefConstructor_ReturnFalse()
        {
            var auth = TestsHelpers.CreateAuth();

            var result = await auth.Login(new UserCredentials());

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsEmpty_ReturnFalse()
        {
            var auth = TestsHelpers.CreateAuth();

            var result = await auth.Login(new UserCredentials(string.Empty, string.Empty));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsNotValid_ReturnFalse()
        {
            var auth = TestsHelpers.CreateAuth();

            var result = await auth.Login(new UserCredentials("aa", "bb"));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsInactiveUser_ReturnFalse()
        {
            var auth = TestsHelpers.CreateAuth();

            var result = await auth.Login(new UserCredentials(TestsConstants.UserUsernameInactive, TestsConstants.UserPasswordInactive));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsValid_ReturnTrue()
        {
            var auth = TestsHelpers.CreateAuth();

            var result = await auth.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));

            Assert.True(result.Success);
        }
        #endregion

        #region RefershToken
        [Fact]
        public async Task RefreshToken_Null_ReturnFalse()
        {
            var auth = TestsHelpers.CreateAuth();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await auth.RefreshToken(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Empty_ReturnFalse()
        {
            var auth = TestsHelpers.CreateAuth();

            var result = await auth.RefreshToken(string.Empty);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_NotValid_ReturnFalse()
        {
            var auth = TestsHelpers.CreateAuth();

            var result = await auth.RefreshToken("aa");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Expired_ReturnFalse()
        {
            var auth = TestsHelpers.CreateAuth();

            var result = await auth.RefreshToken(TestsConstants.TokenExpired);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_InactiveUser_ReturnFalse()
        {
            var userToken = TestsHelpers.CreateTokenInactiveUser();
            var auth = TestsHelpers.CreateAuth();

            var result = await auth.RefreshToken(userToken.Token);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Valid_ReturnTrue()
        {
            var userToken = TestsHelpers.CreateTokenValid();
            var auth = TestsHelpers.CreateAuth();

            var result = await auth.RefreshToken(userToken.Token);

            Assert.True(result.Success);
        }
        #endregion

        #region IsGranted
        [Fact]
        public async Task IsGranted_NullGetListState_ReturnGranted()
        {
            var auth = TestsHelpers.CreateAuth();

            var result = await auth.IsGranted(null, PermissionType.GetList, PermissionDataType.State);

            Assert.Equal(PermissionResult.Granted, result);
        }

        [Fact]
        public async Task IsGranted_UserGetListState_ReturnGranted()
        {
            var auth = TestsHelpers.CreateAuth();
            var userIdentity = new UserIdentity()
            {
                Id = 1,
                Role = Role.User,
            };

            var result = await auth.IsGranted(userIdentity, PermissionType.GetList, PermissionDataType.State);

            Assert.Equal(PermissionResult.Granted, result);
        }

        [Fact]
        public async Task IsGranted_AdminGetListState_ReturnGranted()
        {
            var auth = TestsHelpers.CreateAuth();
            var userIdentity = new UserIdentity()
            {
                Id = 1,
                Role = Role.Admin,
            };

            var result = await auth.IsGranted(userIdentity, PermissionType.GetList, PermissionDataType.State);

            Assert.Equal(PermissionResult.Granted, result);
        }

        [Fact]
        public async Task IsGranted_UserAddState_ReturnNotGranted()
        {
            var auth = TestsHelpers.CreateAuth();
            var userIdentity = new UserIdentity()
            {
                Id = 1,
                Role = Role.User,
            };

            var result = await auth.IsGranted(userIdentity, PermissionType.Add, PermissionDataType.State);

            Assert.Equal(PermissionResult.NotGranted, result);
        }

        [Fact]
        public async Task IsGranted_AdminAddState_ReturnGranted()
        {
            var auth = TestsHelpers.CreateAuth();
            var userIdentity = new UserIdentity()
            {
                Id = 1,
                Role = Role.Admin,
            };

            var result = await auth.IsGranted(userIdentity, PermissionType.Add, PermissionDataType.State);

            Assert.Equal(PermissionResult.Granted, result);
        }
        #endregion
    }
}
