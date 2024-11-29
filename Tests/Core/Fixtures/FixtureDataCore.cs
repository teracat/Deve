using Deve.Auth.Jwt;
using Deve.Core;

namespace Deve.Tests.Core
{
    public class FixtureDataCore : IFixtureData<ICore>
    {
        public ICore Data { get; private set; }

        public FixtureDataCore()
        {
            //IsSharedInstance is set to true so the Login stores the User authenticated to avoid permissions errors
            Data = new CoreMain(true, TestsHelpers.CreateDataSourceMock(), null, new TokenManagerJwt());
        }
    }
}
