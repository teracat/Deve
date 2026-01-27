using Deve.Cache;

namespace Deve.Tests.Cache.Fixtures;

public class SimpleInMemoryCacheFixture : ICacheFixture
{
    public ICache Cache { get; }

    public SimpleInMemoryCacheFixture()
    {
        Cache = new SimpleInMemoryCache();
    }
}
