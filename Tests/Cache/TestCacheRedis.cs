using Deve.Tests.Cache.Fixtures;

namespace Deve.Tests.Cache
{
    public class TestCacheRedis : TestCacheBase, IClassFixture<FixtureRedisCache>
    {
        public TestCacheRedis(FixtureRedisCache fixtureCache)
            : base(fixtureCache)
        {   
        }
    }
}
