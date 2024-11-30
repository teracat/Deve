using Deve.Core;

namespace Deve.Tests.Core
{
    public abstract class TestCoreBaseDataGet<ModelList, Model, Criteria> : TestBaseDataGet<ICore, ModelList, Model, Criteria>, IClassFixture<FixtureDataCore> where Model: ModelId
    {
        public TestCoreBaseDataGet(FixtureDataCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }
    }
}