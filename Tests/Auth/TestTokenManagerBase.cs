using Xunit;
using Deve.Auth;

namespace Deve.Tests.Auth
{
    /// <summary>
    /// TokenManager Tests definitions.
    /// </summary>
    public abstract class TestTokenManagerBase
    {
        protected abstract ITokenManager CreateTokenManager();
        protected abstract string GetExpiredToken();

        [Fact]
        public void CreateToken_Null_ThrowsArgumentNullException()
        {
            var tokenManager = CreateTokenManager();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(() => tokenManager.CreateToken(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void CreateToken_User_NotNull()
        {
            var tokenManager = CreateTokenManager();

            var userToken = tokenManager.CreateToken(new UserTests());
            
            Assert.NotNull(userToken);
        }

        [Fact]
        public void ValidateToken_Null_NotValid()
        {
            var tokenManager = CreateTokenManager();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = tokenManager.ValidateToken(null, out _);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.Equal(TokenParseResult.NotValid, result);
        }

        [Fact]
        public void ValidateToken_Empty_NotValid()
        {
            var tokenManager = CreateTokenManager();

            var result = tokenManager.ValidateToken(string.Empty, out _);

            Assert.Equal(TokenParseResult.NotValid, result);
        }

        [Fact]
        public void ValidateToken_String_NotValid()
        {
            var tokenManager = CreateTokenManager();

            var result = tokenManager.ValidateToken("not valid", out _);

            Assert.Equal(TokenParseResult.NotValid, result);
        }

        [Fact]
        public void ValidateToken_ExpiredToken_Expired()
        {
            var tokenManager = CreateTokenManager();

            var result = tokenManager.ValidateToken(GetExpiredToken(), out _);

            Assert.Equal(TokenParseResult.Expired, result);
        }

        [Fact]
        public void ValidateToken_User_Valid()
        {
            var tokenManager = CreateTokenManager();
            UserToken userToken = tokenManager.CreateToken(new UserTests());

            var result = tokenManager.ValidateToken(userToken.Token, out _);

            Assert.Equal(TokenParseResult.Valid, result);
        }
    }
}
