﻿using Deve.Auth;

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
        public async Task Login_CredentialsValid_ReturnTrue()
        {
            var auth = TestsHelpers.CreateAuth();
            var result = await auth.Login(new UserCredentials("teracat", "teracat"));

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

            var result = await auth.RefreshToken("P83hovvDJI9+6LMyV9Tv/MCBELipU06iTIqm9IqsTTMNjLPaYmSvarlIxOst+2ZId4dHPK2xkqKD1hQL6Iy3gf7DEg8y+3N2K4REL2A0FVA=");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Valid_ReturnTrue()
        {
            var ds = TestsHelpers.CreateDataSource();
            var auth = TestsHelpers.CreateAuth(ds);

            var usersRes = await ds.Users.Get(new Internal.CriteriaUser()
            {
                OnlyActive = CriteriaActiveType.OnlyActive
            });
            if (!usersRes.Success)
            {
                Assert.True(usersRes.Success, "Could not find an active user");
                return;
            }
            if (usersRes.Data.Count == 0)
            {
                Assert.NotEmpty(usersRes.Data);
                return;
            }

            var tokenData = auth.TokenManager.CreateToken(usersRes.Data.First());
            var result = await auth.RefreshToken(tokenData.Token);

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
