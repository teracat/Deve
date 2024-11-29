using Deve.Core;
using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreState : TestState<ICore>, IClassFixture<FixtureDataCore>, IClassFixture<FixtureDataCoreLogged>
    {
        public TestCoreState(FixtureDataCore fixtureDataCore, FixtureDataCoreLogged fixtureDataLogged)
            : base(fixtureDataCore, fixtureDataLogged)
        {
        }

        protected override IDataAll<State, State, CriteriaState> GetDataAll(ICore core) => core.States;
    }
}