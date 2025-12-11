using Deve.Core;
using Deve.Dto;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core
{
    public abstract class TestCoreBaseDataAll<ModelList, Model, Criteria> : TestBaseDataAll<ICore, ModelList, Model, Criteria>, IClassFixture<FixtureCore> where Model : ModelId
    {
        protected TestCoreBaseDataAll(FixtureCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }
    }
}