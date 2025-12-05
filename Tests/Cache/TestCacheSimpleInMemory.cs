using Deve.Tests.Cache.Fixtures;

namespace Deve.Tests.Cache
{
    public class TestCacheSimpleInMemory : TestCacheBase, IClassFixture<FixtureSimpleInMemoryCache>
    {
        public TestCacheSimpleInMemory(FixtureSimpleInMemoryCache fixtureCache)
            : base(fixtureCache)
        {
        }
    }
}
