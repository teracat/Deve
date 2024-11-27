using Deve.Core;
using Deve.Internal;

namespace Deve.Tests.Core
{
    public class TestCoreState : TestState<ICore>
    {
        protected override ICore CreateData() => TestsCoreHelpers.CreateCore();

        protected override IDataAll<State, State, CriteriaState> GetDataAll(ICore core) => core.States;
    }
}