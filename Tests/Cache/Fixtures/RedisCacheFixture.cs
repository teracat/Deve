using Deve.Cache;
using Deve.Tests.Cache.Mocks;

namespace Deve.Tests.Cache.Fixtures;

public class RedisCacheFixture : ICacheFixture
{
    public ICache Cache { get; }

    public RedisCacheFixture()
    {
        Cache = new RedisCache(new RedisDatabaseMock().Object);
    }
}
