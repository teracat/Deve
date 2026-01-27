using Deve.Tests.Cache.Fixtures;

namespace Deve.Tests.Cache;

public class SimpleInMemoryCacheTest : BaseCacheTest, IClassFixture<SimpleInMemoryCacheFixture>
{
    public SimpleInMemoryCacheTest(SimpleInMemoryCacheFixture fixtureCache)
        : base(fixtureCache)
    {
    }
}
