using Deve.Tests.Auth.Fixtures;

namespace Deve.Tests.Auth
{
    /// <summary>
    /// TokenManager Tests definitions.
    /// </summary>
    public abstract class TestTokenManagerBase
    {
        private readonly IFixtureTokenManager _fixtureTokenManager;

        protected TestTokenManagerBase(IFixtureTokenManager fixtureTokenManager)
        {
            _fixtureTokenManager = fixtureTokenManager;
        }

        [Fact]
        public void CreateToken_Null_ThrowsArgumentNullException() =>
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(() => _fixtureTokenManager.TokenManager.CreateToken(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.


        [Fact]
        public void CreateToken_User_NotNull()
        {
            var userToken = _fixtureTokenManager.TokenManager.CreateToken(new UserTests());

            Assert.NotNull(userToken);
        }

        [Fact]
        public void ValidateToken_Null_ReturnsFalse()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = _fixtureTokenManager.TokenManager.TryValidateToken(null, out _);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.False(result);
        }

        [Fact]
        public void ValidateToken_Empty_ReturnsFalse()
        {
            var result = _fixtureTokenManager.TokenManager.TryValidateToken(string.Empty, out _);

            Assert.False(result);
        }

        [Fact]
        public void ValidateToken_InvalidString_ReturnsFalse()
        {
            var result = _fixtureTokenManager.TokenManager.TryValidateToken("not valid", out _);

            Assert.False(result);
        }

        [Fact]
        public void ValidateToken_ExpiredToken_ReturnsFalse()
        {
            var result = _fixtureTokenManager.TokenManager.TryValidateToken(_fixtureTokenManager.TokenExpired, out _);

            Assert.False(result);
        }

        [Fact]
        public void ValidateToken_ValidUser_ReturnsTrue()
        {
            var userToken = _fixtureTokenManager.TokenManager.CreateToken(new UserTests());

            var result = _fixtureTokenManager.TokenManager.TryValidateToken(userToken.Token, out _);

            Assert.True(result);
        }

        [Fact]
        public void ValidateToken_ValidUserNotNullToken()
        {
            var userToken = _fixtureTokenManager.TokenManager.CreateToken(new UserTests());

            _ = _fixtureTokenManager.TokenManager.TryValidateToken(userToken.Token, out var userIdentity);

            Assert.NotNull(userIdentity);
        }
    }
}