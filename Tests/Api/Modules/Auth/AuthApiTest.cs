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

    #region Common
    [Theory]
    [InlineData(ApiConstants.PathAuthV1 + ApiConstants.MethodLogin)]
    [InlineData(ApiConstants.PathAuthV1 + ApiConstants.MethodRefreshToken)]
    public async Task Get_NullRequest_NotSuccessStatusCode(string path)
    {
        var response = await Fixture.ClientNoAuth.PostAsync(path, null);

        Assert.False(response.IsSuccessStatusCode);
    }
    #endregion

    #region Login
    [Fact]
    public async Task Login_EmptyCredentials_NotSuccessStatusCode()
    {
        var data = new LoginRequest("", "");
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(ApiConstants.PathAuthV1 + ApiConstants.MethodLogin, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Login_NotValidCredentials_NotSuccessStatusCode()
    {
        var data = new LoginRequest("aa", "aa");
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(ApiConstants.PathAuthV1 + ApiConstants.MethodLogin, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Login_InactiveUserCredentials_NotSuccessStatusCode()
    {
        var data = new LoginRequest(TestsConstants.UserUsernameInactive, TestsConstants.UserPasswordInactive);
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(ApiConstants.PathAuthV1 + ApiConstants.MethodLogin, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task Login_ValidCredentials_SuccessStatusCode()
    {
        var data = new LoginRequest(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid);
        using var clientNoAuth = Fixture.CreateClient();  // We don't want to keep the useridentity
        using var httpContent = ToHttpContent(data);

        var response = await clientNoAuth.PostAsync(ApiConstants.PathAuthV1 + ApiConstants.MethodLogin, httpContent);

        Assert.True(response.IsSuccessStatusCode);
    }
    #endregion

    #region RefreshToken
    [Fact]
    public async Task RefreshToken_NullRequest_NotSuccessStatusCode()
    {
        var response = await Fixture.ClientNoAuth.PostAsync(ApiConstants.PathAuthV1 + ApiConstants.MethodRefreshToken, null);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task RefreshToken_EmptyToken_NotSuccessStatusCode()
    {
        var data = new RefreshTokenRequest("");
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(ApiConstants.PathAuthV1 + ApiConstants.MethodRefreshToken, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task RefreshToken_NotValidToken_NotSuccessStatusCode()
    {
        var data = new RefreshTokenRequest("aa");
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(ApiConstants.PathAuthV1 + ApiConstants.MethodRefreshToken, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task RefreshToken_ExpiredToken_NotSuccessStatusCode()
    {
        var data = new RefreshTokenRequest(TestsConstants.TokenExpired);
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(ApiConstants.PathAuthV1 + ApiConstants.MethodRefreshToken, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task RefreshToken_InactiveUser_NotSuccessStatusCode()
    {
        var data = new RefreshTokenRequest(Fixture.UserTokenInactiveUser.Token);
        using var httpContent = ToHttpContent(data);

        var response = await Fixture.ClientNoAuth.PostAsync(ApiConstants.PathAuthV1 + ApiConstants.MethodRefreshToken, httpContent);

        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task RefreshToken_Valid_SuccessStatusCode()
    {
        var data = new RefreshTokenRequest(Fixture.UserTokenValid.Token);
        using var client = Fixture.CreateClient(); // We don't want to keep the useridentity
        using var httpContent = ToHttpContent(data);

        var response = await client.PostAsync(ApiConstants.PathAuthV1 + ApiConstants.MethodRefreshToken, httpContent);

        Assert.True(response.IsSuccessStatusCode);
    }
    #endregion
}
