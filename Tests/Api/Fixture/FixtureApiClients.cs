using Deve.Auth.TokenManagers;
using Deve.Authenticate;

namespace Deve.Tests.Api.Fixture
{
    public class FixtureApiClients<TEntryPoint> : FixtureApi<TEntryPoint> where TEntryPoint: class
    {
        public ITokenManager TokenManager { get; }

        public HttpClient ClientNoAuth { get; private set; }
        public HttpClient ClientValidAuth { get; private set; }
        public HttpClient ClientInvalidUser { get; private set; }

        public UserToken UserTokenValid { get; private set; }
        public UserToken UserTokenInactiveUser { get; private set; }

        public FixtureApiClients()
        {
            // TokenManager
            TokenManager = TestsHelpers.CreateTokenManager();

            // Tokens
            UserTokenInactiveUser = TestsHelpers.CreateTokenInactiveUser(TokenManager);
            UserTokenValid = TestsHelpers.CreateTokenValid(TokenManager);

            // Clients
            ClientNoAuth = _factory.CreateClient();
            ClientValidAuth = _factory.CreateClient();
            ClientInvalidUser = _factory.CreateClient();

            // Authorization
            ClientValidAuth.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(UserTokenValid.Scheme, UserTokenValid.Token);
            ClientInvalidUser.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(UserTokenInactiveUser.Scheme, UserTokenInactiveUser.Token);
        }

        protected override void Dispose(bool disposing)
        {
            ClientNoAuth.Dispose();
            ClientValidAuth.Dispose();
            ClientInvalidUser.Dispose();
            TokenManager.Dispose();
            base.Dispose(disposing);
        }
    }
}