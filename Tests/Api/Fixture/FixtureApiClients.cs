using Deve.Auth.TokenManagers;
using Deve.Auth;
using Deve.Identity.Enums;

namespace Deve.Tests.Api.Fixture;

public class FixtureApiClients : FixtureApi
{
    public ITokenManager TokenManager { get; }

    public HttpClient ClientNoAuth { get; }
    public HttpClient ClientAuthUser { get; }
    public HttpClient ClientAuthAdmin { get; }
    public HttpClient ClientInvalidUser { get; }

    public UserToken UserTokenValid { get; }
    public UserToken UserTokenInactiveUser { get; }
    public UserToken AdminTokenValid { get; }

    public FixtureApiClients()
    {
        // TokenManager
        TokenManager = TestsHelpers.CreateTokenManager();

        // Tokens
        UserTokenInactiveUser = TestsHelpers.CreateTokenInactiveUser(TokenManager, Role.User);
        UserTokenValid = TestsHelpers.CreateTokenValid(TokenManager, Role.User);
        AdminTokenValid = TestsHelpers.CreateTokenValid(TokenManager, Role.Admin);

        // Clients
        ClientNoAuth = CreateClient();
        ClientAuthUser = CreateClient();
        ClientAuthAdmin = CreateClient();
        ClientInvalidUser = CreateClient();

        // Authorization
        ClientAuthUser.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(UserTokenValid.Scheme, UserTokenValid.Token);
        ClientAuthAdmin.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(AdminTokenValid.Scheme, AdminTokenValid.Token);
        ClientInvalidUser.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(UserTokenInactiveUser.Scheme, UserTokenInactiveUser.Token);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            ClientNoAuth?.Dispose();
            ClientAuthUser?.Dispose();
            ClientAuthAdmin?.Dispose();
            ClientInvalidUser?.Dispose();
            TokenManager?.Dispose();
        }
        base.Dispose(disposing);
    }
}
