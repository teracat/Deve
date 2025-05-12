using Deve.Authenticate;
using Deve.Core;

namespace Deve.Tests.Core.Fixtures
{
    public class FixtureCore : FixtureCommon, IFixtureData<ICore>
    {
        public ICore DataNoAuth { get; private set; }
        public ICore DataValidAuth { get; private set; }

        public FixtureCore()
        {
            DataNoAuth = new CoreMain(DataSource, Auth, Options);
            DataValidAuth = new CoreMain(DataSource, Auth, Options);
        }

        public override async Task InitializeAsync()
        {
            await DataValidAuth.Authenticate.Login(new UserCredentials(TestsConstants.UserUsernameValid, TestsConstants.UserPasswordValid));
        }

        public override Task DisposeAsync()
        {
            DataNoAuth.Dispose();
            DataValidAuth.Dispose();
            return base.DisposeAsync();
        }
    }
}