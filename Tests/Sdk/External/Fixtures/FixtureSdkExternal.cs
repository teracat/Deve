using Deve.External.Api;
using Deve.External.Sdk;
using Deve.Tests.Sdk.Fixtures;

namespace Deve.Tests.Sdk.External.Fixtures
{
    public class FixtureSdkExternal : FixtureSdk<Program, ISdk>
    {
        public FixtureSdkExternal() 
            : base(new FixtureSdkDataTypeBuilderInternal())
        {
        }
    }
}