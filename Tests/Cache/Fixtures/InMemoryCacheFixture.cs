using Deve.Cache;

namespace Deve.Tests.Cache.Fixtures;

public class InMemoryCacheFixture : ICacheFixture
{
    public ICache Cache { get; }

    public InMemoryCacheFixture()
    {
        Cache = new InMemoryCache();
    }
}
