using Deve.Core;

namespace Deve.Tests.Core
{
    public abstract class TestCoreBaseDataGet<ModelList, Model, Criteria> : TestBaseDataGet<ICore, ModelList, Model, Criteria>, IClassFixture<FixtureDataCore>, IClassFixture<FixtureDataCoreLogged> where Model: ModelId
    {
        public TestCoreBaseDataGet(FixtureDataCore fixtureDataCore, FixtureDataCoreLogged fixtureDataLogged)
            : base(fixtureDataCore, fixtureDataLogged)
        {
        }
    }
}