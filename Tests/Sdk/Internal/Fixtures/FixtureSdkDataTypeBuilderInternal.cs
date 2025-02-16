using Microsoft.AspNetCore.Mvc.Testing;
using Deve.Internal.Api;
using Deve.Internal.Sdk;
using Deve.Tests.Sdk.Fixtures;

namespace Deve.Tests.Sdk.Internal.Fixtures
{
    public class FixtureSdkDataTypeBuilderInternal : IFixtureSdkDataTypeBuilderExternal<Program, ISdk>
    {
        public ISdk CreateData(WebApplicationFactory<Program> factory) => new SdkMain(factory.CreateClient());
    }
}