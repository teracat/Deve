using Deve.Core;
using Deve.Model;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core
{
    public abstract class TestCoreBaseDataAll<ModelList, Model, Criteria> : TestBaseDataAll<ICore, ModelList, Model, Criteria>, IClassFixture<FixtureDataCore> where Model: ModelId
    {
        public TestCoreBaseDataAll(FixtureDataCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }
    }
}