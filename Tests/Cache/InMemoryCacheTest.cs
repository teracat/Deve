using Deve.Tests.Cache.Fixtures;

namespace Deve.Tests.Cache;

public class InMemoryCacheTest : BaseCacheTest, IClassFixture<InMemoryCacheFixture>
{
    public InMemoryCacheTest(InMemoryCacheFixture fixtureCache)
        : base(fixtureCache)
    {
    }
}
