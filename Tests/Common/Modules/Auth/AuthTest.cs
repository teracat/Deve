using Deve.Data;
using Deve.Auth.Login;
using Deve.Auth.RefreshToken;
using Deve.Identity.Enums;

namespace Deve.Tests.Modules.Auth;

/// <summary>
/// Auth Tests.
/// </summary>
public abstract class AuthTest : BaseTest<IData>
{
    protected AuthTest(IDataFixture<IData> fixture)
        : base(fixture)
    {
    }

    [Fact]
    public async Task Login_CredentialsNull_ReturnFalse()
    {
        var result = await Fixture.DataNoAuth.Auth.Login(null, TestContext.Current.CancellationToken);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task Login_CredentialsEmpty_ReturnFalse()
    {
        var result = await Fixture.DataNoAuth.Auth.Login(new LoginRequest(string.Empty, string.Empty), TestContext.Current.CancellationToken);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task Login_CredentialsNotValid_ReturnFalse()
    {
        var result = await Fixture.DataNoAuth.Auth.Login(new LoginRequest("aa", "bb"), TestContext.Current.CancellationToken);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task Login_CredentialsInactive_ReturnFalse()
    {
        var result = await Fixture.DataNoAuth.Auth.Login(new LoginRequest(TestsConstants.UserUsernameInactive, TestsConstants.UserPasswordInactive), TestContext.Current.CancellationToken);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task Login_CredentialsValid_ReturnTrue()
    {
        var result = await Fixture.DataNoAuth.Auth.Login(new LoginRequest(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid), TestContext.Current.CancellationToken);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task RefreshToken_Null_ReturnFalse()
    {
        var result = await Fixture.DataNoAuth.Auth.RefreshToken(null, TestContext.Current.CancellationToken);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task RefreshToken_Empty_ReturnFalse()
    {
        var result = await Fixture.DataNoAuth.Auth.RefreshToken(new RefreshTokenRequest(string.Empty), TestContext.Current.CancellationToken);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task RefreshToken_NotValid_ReturnFalse()
    {
        var result = await Fixture.DataNoAuth.Auth.RefreshToken(new RefreshTokenRequest("aa"), TestContext.Current.CancellationToken);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task RefreshToken_Expired_ReturnFalse()
    {
        var result = await Fixture.DataNoAuth.Auth.RefreshToken(new RefreshTokenRequest(TestsConstants.TokenExpired), TestContext.Current.CancellationToken);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task RefreshToken_InactiveUser_ReturnFalse()
    {
        var userToken = TestsHelpers.CreateTokenInactiveUser(Fixture.TokenManager, Role.User);

        var result = await Fixture.DataNoAuth.Auth.RefreshToken(new RefreshTokenRequest(userToken.Token), TestContext.Current.CancellationToken);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task RefreshToken_Valid_ReturnTrue()
    {
        var userToken = TestsHelpers.CreateTokenValid(Fixture.TokenManager, Role.User);

        var result = await Fixture.DataNoAuth.Auth.RefreshToken(new RefreshTokenRequest(userToken.Token), TestContext.Current.CancellationToken);

        Assert.True(result.Success);
    }
}
