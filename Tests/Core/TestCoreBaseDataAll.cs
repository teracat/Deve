using Deve.Core;

namespace Deve.Tests.Core
{
    public abstract class TestCoreBaseDataAll<ModelList, Model, Criteria> : TestBaseDataAll<ICore, ModelList, Model, Criteria>, IClassFixture<FixtureDataCore>, IClassFixture<FixtureDataCoreLogged> where Model: ModelId
    {
        public TestCoreBaseDataAll(FixtureDataCore fixtureDataCore, FixtureDataCoreLogged fixtureDataLogged)
            : base(fixtureDataCore, fixtureDataLogged)
        {
        }
    }
}