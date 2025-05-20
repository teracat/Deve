using Deve.Tests.Cache.Fixtures;

namespace Cache
{
    public class TestCacheSimpleInMemory : TestCacheBase, IClassFixture<FixtureSimpleInMemoryCache>
    {
        public TestCacheSimpleInMemory(FixtureSimpleInMemoryCache fixtureCache)
            : base(fixtureCache)
        {   
        }
    }
}
