using Deve.Tests.Cache.Fixtures;

namespace Deve.Tests.Cache
{
    public class TestCacheInMemory : TestCacheBase, IClassFixture<FixtureInMemoryCache>
    {
        public TestCacheInMemory(FixtureInMemoryCache fixtureCache)
            : base(fixtureCache)
        {   
        }
    }
}
