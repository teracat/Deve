using Deve.Auth.TokenManagers;
using Deve.Tests.Auth.Fixtures;

namespace Deve.Tests.Auth
{
    /// <summary>
    /// TokenManager Tests definitions.
    /// </summary>
    public abstract class TestTokenManagerBase
    {
        private readonly IFixtureTokenManager _fixtureTokenManager;

        public TestTokenManagerBase(IFixtureTokenManager fixtureTokenManager)
        {
            _fixtureTokenManager = fixtureTokenManager;
        }

        [Fact]
        public void CreateToken_Null_ThrowsArgumentNullException()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(() => _fixtureTokenManager.TokenManager.CreateToken(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void CreateToken_User_NotNull()
        {
            var userToken = _fixtureTokenManager.TokenManager.CreateToken(new UserTests());
            
            Assert.NotNull(userToken);
        }

        [Fact]
        public void ValidateToken_Null_NotValid()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = _fixtureTokenManager.TokenManager.ValidateToken(null, out _);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.Equal(TokenParseResult.NotValid, result);
        }

        [Fact]
        public void ValidateToken_Empty_NotValid()
        {
            var result = _fixtureTokenManager.TokenManager.ValidateToken(string.Empty, out _);

            Assert.Equal(TokenParseResult.NotValid, result);
        }

        [Fact]
        public void ValidateToken_String_NotValid()
        {
            var result = _fixtureTokenManager.TokenManager.ValidateToken("not valid", out _);

            Assert.Equal(TokenParseResult.NotValid, result);
        }

        [Fact]
        public void ValidateToken_ExpiredToken_Expired()
        {
            var result = _fixtureTokenManager.TokenManager.ValidateToken(_fixtureTokenManager.TokenExpired, out _);

            Assert.Equal(TokenParseResult.Expired, result);
        }

        [Fact]
        public void ValidateToken_User_Valid()
        {
            var userToken = _fixtureTokenManager.TokenManager.CreateToken(new UserTests());

            var result = _fixtureTokenManager.TokenManager.ValidateToken(userToken.Token, out _);

            Assert.Equal(TokenParseResult.Valid, result);
        }
    }
}