using Deve.Core;
using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreState : TestState<ICore>, IClassFixture<FixtureDataCore>
    {
        public TestCoreState(FixtureDataCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }

        protected override IDataAll<State, State, CriteriaState> GetDataAll(ICore core) => core.States;
    }
}