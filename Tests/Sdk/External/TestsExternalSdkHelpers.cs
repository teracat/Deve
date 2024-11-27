using Deve.External.Sdk;

namespace Deve.Tests.Sdk.External
{
    internal static class TestsExternalSdkHelpers
    {
        public static ISdk CreateSdk(HttpClient client) => new SdkMain(client);
    }
}
