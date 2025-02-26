using Deve.Core;
using Deve.Criteria;
using Deve.Model;
using Deve.Internal.Data;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core
{
    public class TestCoreState : TestState<ICore>, IClassFixture<FixtureCore>
    {
        public TestCoreState(FixtureCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }

        protected override IDataAll<State, State, CriteriaState> GetDataAll(ICore core) => core.States;
    }
}