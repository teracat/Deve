using Xunit;
using Deve.Auth;

namespace Deve.Tests.Auth
{
    /// <summary>
    /// TokenManager Tests.
    /// </summary>
    public class TestTokenManager
    {
        [Fact]
        public void CreateToken_Null_ThrowsArgumentNullException()
        {
            var auth = AuthFactory.Get();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.Throws<ArgumentNullException>(() => auth.TokenManager.CreateToken(null));
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        [Fact]
        public void CreateToken_User_NotNull()
        {
            var auth = AuthFactory.Get();

            var userToken = auth.TokenManager.CreateToken(new UserTests());
            
            Assert.NotNull(userToken);
        }
    }
}
