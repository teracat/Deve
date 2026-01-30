using Deve.Auth;
using Deve.Identity.Enums;
using Deve.Tests.Auth.Fixtures;

namespace Deve.Tests.Auth;

/// <summary>
/// TokenManager Tests definitions.
/// </summary>
public abstract class TokenManagerBaseTest
{
    private readonly ITokenManagerFixture _fixtureTokenManager;
    private readonly UserIdentity _userIdentity = new(
        Guid.Parse("3c4d5e6f-7081-9201-2345-67890abcdef2"),
        "user.tests",
        Role.Admin);

    protected TokenManagerBaseTest(ITokenManagerFixture fixtureTokenManager)
    {
        _fixtureTokenManager = fixtureTokenManager;
    }

    [Fact]
    public void CreateToken_Null_ThrowsArgumentNullException() =>
        Assert.Throws<ArgumentNullException>(() => _fixtureTokenManager.TokenManager.CreateToken(null));


    [Fact]
    public void CreateToken_User_NotNull()
    {
        var userToken = _fixtureTokenManager.TokenManager.CreateToken(_userIdentity);

        Assert.NotNull(userToken);
    }

    [Fact]
    public void ValidateToken_Null_ReturnsFalse()
    {
        var result = _fixtureTokenManager.TokenManager.TryValidateToken(null, out _);

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
        var userToken = _fixtureTokenManager.TokenManager.CreateToken(_userIdentity);

        var result = _fixtureTokenManager.TokenManager.TryValidateToken(userToken.Token, out _);

        Assert.True(result);
    }

    [Fact]
    public void ValidateToken_ValidUserNotNullToken()
    {
        var userToken = _fixtureTokenManager.TokenManager.CreateToken(_userIdentity);

        _ = _fixtureTokenManager.TokenManager.TryValidateToken(userToken.Token, out var userIdentity);

        Assert.NotNull(userIdentity);
    }
}
