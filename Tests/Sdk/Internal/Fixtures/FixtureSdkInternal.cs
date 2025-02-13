using Deve.Internal.Api;
using Deve.Internal.Sdk;
using Deve.Tests.Sdk.Fixtures;

namespace Deve.Tests.Sdk.Internal.Fixtures
{
    public class FixtureSdkInternal : FixtureSdk<Program, ISdk>, IFixtureData<ISdk>
    {
        protected override ISdk CreateData() => new SdkMain(_factory.CreateClient());
    }
}