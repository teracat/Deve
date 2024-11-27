using Deve.Core;

namespace Deve.Tests.Core
{
    public abstract class TestCoreBaseDataGet<ModelList, Model, Criteria> : TestBaseDataGet<ICore, ModelList, Model, Criteria> where Model: ModelId
    {
        protected override ICore CreateData() => TestsCoreHelpers.CreateCore();
    }
}