namespace Deve.Tests.Core
{
    /// <summary>
    /// Authenticate Tests.
    /// </summary>
    public class TestAuthenticate : TestCoreBase
    {
        [Fact]
        public async Task Login_CredentialsNull_ReturnFalse()
        {
            var core = CreateCore();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await core.Authenticate.Login(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsDefConstructor_ReturnFalse()
        {
            var core = CreateCore();

            var result = await core.Authenticate.Login(new UserCredentials());

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsEmpty_ReturnFalse()
        {
            var core = CreateCore();

            var result = await core.Authenticate.Login(new UserCredentials(string.Empty, string.Empty));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsNotValid_ReturnFalse()
        {
            var core = CreateCore();

            var result = await core.Authenticate.Login(new UserCredentials("aa", "bb"));

            Assert.False(result.Success);
        }

        [Fact]
        public async Task Login_CredentialsValid_ReturnTrue()
        {
            var core = CreateCore();

            var result = await core.Authenticate.Login(new UserCredentials(TestsHelpers.ValidUsername, TestsHelpers.ValidPassword));

            Assert.True(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Null_ReturnFalse()
        {
            var core = CreateCore();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await core.Authenticate.RefreshToken(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Empty_ReturnFalse()
        {
            var core = CreateCore();

            var result = await core.Authenticate.RefreshToken(string.Empty);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_NotValid_ReturnFalse()
        {
            var core = CreateCore();

            var result = await core.Authenticate.RefreshToken("aa");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Expired_ReturnFalse()
        {
            var core = CreateCore();

            var result = await core.Authenticate.RefreshToken("P83hovvDJI9+6LMyV9Tv/MCBELipU06iTIqm9IqsTTMNjLPaYmSvarlIxOst+2ZId4dHPK2xkqKD1hQL6Iy3gf7DEg8y+3N2K4REL2A0FVA=");

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_InactiveUser_ReturnFalse()
        {
            var core = CreateCore();
            var usersRes = await core.DataSource.Users.Get(new Internal.CriteriaUser()
            {
                OnlyActive = CriteriaActiveType.OnlyInactive,
            });
            if (!usersRes.Success)
            {
                Assert.True(usersRes.Success, "Could not find any invalid user");
                return;
            }
            if (usersRes.Data.Count == 0)
            {
                Assert.NotEmpty(usersRes.Data);
                return;
            }

            var tokenData = core.Auth.TokenManager.CreateToken(usersRes.Data.First());
            var result = await core.Authenticate.RefreshToken(tokenData.Token);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task RefreshToken_Valid_ReturnTrue()
        {
            var core = CreateCore();
            var usersRes = await core.DataSource.Users.Get(new Internal.CriteriaUser()
            {
                OnlyActive = CriteriaActiveType.OnlyActive
            });
            if (!usersRes.Success)
            {
                Assert.True(usersRes.Success);
                return;
            }
            if (usersRes.Data.Count == 0)
            {
                Assert.NotEmpty(usersRes.Data);
                return;
            }

            var tokenData = core.Auth.TokenManager.CreateToken(usersRes.Data.First());
            var result = await core.Authenticate.RefreshToken(tokenData.Token);

            Assert.True(result.Success);
        }
    }
}
