using Deve.Core;

namespace Deve.Tests.Core
{
    public abstract class TestCoreBaseDataAll<ModelList, Model, Criteria> : TestBaseDataAll<ICore, ModelList, Model, Criteria> where Model: ModelId
    {
        protected override ICore CreateData() => TestsCoreHelpers.CreateCore();
    }
}