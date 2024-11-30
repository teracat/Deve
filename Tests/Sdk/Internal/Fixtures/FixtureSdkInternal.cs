using Deve.Internal.Api;
using Deve.Internal.Sdk;

namespace Deve.Tests.Sdk.Internal
{
    public class FixtureSdkInternal : FixtureSdk<Program, ISdk>, IFixtureData<ISdk>
    {
        public FixtureSdkInternal()
        {
        }

        protected override ISdk CreateData() => new SdkMain(_factory.CreateClient());
    }
}
