using Deve.Core;
using Deve.Dto;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core
{
    public abstract class TestCoreBaseDataGet<ModelList, Model, Criteria> : TestBaseDataGet<ICore, ModelList, Model, Criteria>, IClassFixture<FixtureCore> where Model : ModelId
    {
        protected TestCoreBaseDataGet(FixtureCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }
    }
}