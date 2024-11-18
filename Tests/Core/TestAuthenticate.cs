using Deve.Core;
using Deve.Auth;
using Deve.DataSource;

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
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await Core.Authenticate.Login(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(result.Success, "Login with Null UserCredentials should return false");
        }

        [Fact]
        public async Task Login_CredentialsDefConstructor_ReturnFalse()
        {
            var result = await Core.Authenticate.Login(new UserCredentials());

            Assert.False(result.Success, "Login with Default UserCredentials should return false");
        }

        [Fact]
        public async Task Login_CredentialsEmpty_ReturnFalse()
        {
            var result = await Core.Authenticate.Login(new UserCredentials(string.Empty, string.Empty));

            Assert.False(result.Success, "Login with Empty UserCredentials should return false");
        }

        [Fact]
        public async Task Login_CredentialsNotValid_ReturnFalse()
        {
            var result = await Core.Authenticate.Login(new UserCredentials("aa", "bb"));

            Assert.False(result.Success, "Login with Not Valid UserCredentials should return false");
        }

        [Fact]
        public async Task Login_CredentialsValid_ReturnTrue()
        {
            var result = await ExecuteValidLogin();

            Assert.True(result.Success, "Login with Valid UserCredentials should return true");
        }

        [Fact]
        public async Task RefreshToken_Null_ReturnFalse()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await Core.Authenticate.RefreshToken(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(result.Success, "RefreshToken with Null token should return false");
        }

        [Fact]
        public async Task RefreshToken_Empty_ReturnFalse()
        {
            var result = await Core.Authenticate.RefreshToken(string.Empty);

            Assert.False(result.Success, "RefreshToken with Empty token should return false");
        }

        [Fact]
        public async Task RefreshToken_NotValid_ReturnFalse()
        {
            var result = await Core.Authenticate.RefreshToken("aa");

            Assert.False(result.Success, "RefreshToken with Not Valid token should return false");
        }

        [Fact]
        public async Task RefreshToken_Expired_ReturnFalse()
        {
            var result = await Core.Authenticate.RefreshToken("P83hovvDJI9+6LMyV9Tv/MCBELipU06iTIqm9IqsTTMNjLPaYmSvarlIxOst+2ZId4dHPK2xkqKD1hQL6Iy3gf7DEg8y+3N2K4REL2A0FVA=");

            Assert.False(result.Success, "RefreshToken with Expired token should return false");
        }

        [Fact]
        public async Task RefreshToken_InactiveUser_ReturnFalse()
        {
            var ds = DataSourceFactory.Get();
            var auth = AuthFactory.Get(ds);

            var usersRes = await ds.Users.Get(new Internal.CriteriaUser()
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

            var tokenData = auth.TokenManager.CreateToken(usersRes.Data.First());
            var result = await Core.Authenticate.RefreshToken(tokenData.Token);

            Assert.False(result.Success, "RefreshToken with Invalid User token should return false");
        }

        [Fact]
        public async Task RefreshToken_Valid_ReturnTrue()
        {
            var ds = DataSourceFactory.Get();
            var auth = AuthFactory.Get(ds);

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
            var result = await Core.Authenticate.RefreshToken(tokenData.Token);

            Assert.True(result.Success, "RefreshToken with Valid token should return true");
        }
    }
}
