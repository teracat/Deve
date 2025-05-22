using Deve.Cache;
using Deve.Tests.Cache.Mocks;

namespace Deve.Tests.Cache.Fixtures
{
    public class FixtureRedisCache : IFixtureCache
    {
        public ICache Cache { get; }

        public FixtureRedisCache()
        {
            Cache = new RedisCache(new RedisDatabaseMock().Object);
        }
    }
}
