using Deve.Auth;
using Deve.Auth.Login;
using Deve.Auth.RefreshToken;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Api.Modules.Auth;

/// <summary>
/// Api Auth endpoints tests.
/// </summary>
public class AuthApiTest : BaseApiTest, IClassFixture<FixtureApiClients>
{
    #region Constructor
    public AuthApiTest(FixtureApiClients fixture)
        : base(fixture)
    {
    }
    #endregion

    #region Login
    [Fact]
    public async Task Login_NullRequest_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientNoAuth.PostAsync(AuthConstants.PathAuthV1 + AuthConstants.MethodLogin, null);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Login_EmptyCredentials_NotSuccessStatusCode()
    {
        var data = new LoginRequest("", "");
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(AuthConstants.PathAuthV1 + AuthConstants.MethodLogin, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Login_NotValidCredentials_NotSuccessStatusCode()
    {
        var data = new LoginRequest("aa", "aa");
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(AuthConstants.PathAuthV1 + AuthConstants.MethodLogin, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Login_InactiveUserCredentials_NotSuccessStatusCode()
    {
        var data = new LoginRequest(TestsConstants.UserUsernameInactive, TestsConstants.UserPasswordInactive);
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(AuthConstants.PathAuthV1 + AuthConstants.MethodLogin, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Login_ValidCredentials_SuccessStatusCode()
    {
        var data = new LoginRequest(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid);
        using var clientNoAuth = Fixture.CreateClient();  // We don't want to keep the useridentity
        using var httpContent = ToHttpContent(data);

        var response = await clientNoAuth.PostAsync(AuthConstants.PathAuthV1 + AuthConstants.MethodLogin, httpContent);

        Assert.True(response.IsSuccessStatusCode);
    }
    #endregion

    #region RefreshToken
    [Fact]
    public async Task RefreshToken_NullRequest_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientNoAuth.PostAsync(AuthConstants.PathAuthV1 + AuthConstants.MethodRefreshToken, null);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task RefreshToken_EmptyToken_NotSuccessStatusCode()
    {
        var data = new RefreshTokenRequest("");
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(AuthConstants.PathAuthV1 + AuthConstants.MethodRefreshToken, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task RefreshToken_NotValidToken_NotSuccessStatusCode()
    {
        var data = new RefreshTokenRequest("aa");
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(AuthConstants.PathAuthV1 + AuthConstants.MethodRefreshToken, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task RefreshToken_ExpiredToken_NotSuccessStatusCode()
    {
        var data = new RefreshTokenRequest(TestsConstants.TokenExpired);
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(AuthConstants.PathAuthV1 + AuthConstants.MethodRefreshToken, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task RefreshToken_InactiveUser_NotSuccessStatusCode()
    {
        var data = new RefreshTokenRequest(Fixture.UserTokenInactiveUser.Token);
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(AuthConstants.PathAuthV1 + AuthConstants.MethodRefreshToken, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task RefreshToken_Valid_SuccessStatusCode()
    {
        var data = new RefreshTokenRequest(Fixture.UserTokenValid.Token);
        using var client = Fixture.CreateClient(); // We don't want to keep the useridentity
        using var httpContent = ToHttpContent(data);

        var response = await client.PostAsync(AuthConstants.PathAuthV1 + AuthConstants.MethodRefreshToken, httpContent);

        Assert.True(response.IsSuccessStatusCode);
    }
    #endregion
}
