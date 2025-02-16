using Microsoft.AspNetCore.Mvc.Testing;
using Deve.External.Api;
using Deve.External.Sdk;
using Deve.Tests.Sdk.Fixtures;

namespace Deve.Tests.Sdk.External.Fixtures
{
    public class FixtureSdkDataTypeBuilderInternal : IFixtureSdkDataTypeBuilderExternal<Program, ISdk>
    {
        public ISdk CreateData(WebApplicationFactory<Program> factory) => new SdkMain(factory.CreateClient());
    }
}