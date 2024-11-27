using Deve.Internal.Sdk;

namespace Deve.Tests.Sdk.Internal
{
    internal static class TestsInternalSdkHelpers
    {
        public static ISdk CreateSdk(HttpClient client) => new SdkMain(client);
    }
}
