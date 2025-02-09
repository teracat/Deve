using Deve.Authenticate;

namespace Deve.Tests.Api.Fixture
{
    public class FixtureApiClients<TEntryPoint> : FixtureApi<TEntryPoint> where TEntryPoint: class
    {
        public HttpClient ClientNoAuth { get; private set; }
        public HttpClient ClientValidAuth { get; private set; }
        public HttpClient ClientInvalidUser { get; private set; }

        public UserToken UserTokenValid { get; private set; }
        public UserToken UserTokenInactiveUser { get; private set; }

        public FixtureApiClients()
        {
            // Tokens
            UserTokenInactiveUser = TestsHelpers.CreateTokenInactiveUser();
            UserTokenValid = TestsHelpers.CreateTokenValid();

            // Clients
            ClientNoAuth = _factory.CreateClient();
            ClientValidAuth = _factory.CreateClient();
            ClientInvalidUser = _factory.CreateClient();

            // Authorization
            ClientValidAuth.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(UserTokenValid.Scheme, UserTokenValid.Token);
            ClientInvalidUser.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(UserTokenInactiveUser.Scheme, UserTokenInactiveUser.Token);
        }
    }
}