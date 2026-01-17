using Deve.Tests.Cache.Fixtures;

namespace Deve.Tests.Cache;

public class RedisCacheTest : BaseCacheTest, IClassFixture<RedisCacheFixture>
{
    public RedisCacheTest(RedisCacheFixture fixtureCache)
        : base(fixtureCache)
    {
    }
}
