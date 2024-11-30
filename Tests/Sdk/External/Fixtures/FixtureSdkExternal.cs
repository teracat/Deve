using Deve.External.Api;
using Deve.External.Sdk;

namespace Deve.Tests.Sdk.External
{
    public class FixtureSdkExternal : FixtureSdk<Program, ISdk>, IFixtureData<ISdk>
    {
        public FixtureSdkExternal()
        {
        }

        protected override ISdk CreateData() => new SdkMain(_factory.CreateClient());
    }
}