using Deve.Cache;

namespace Deve.Tests.Cache.Fixtures
{
    public class FixtureSimpleInMemoryCache : IFixtureCache
    {
        public ICache Cache { get; }

        public FixtureSimpleInMemoryCache()
        {
            Cache = new SimpleInMemoryCache();
        }
    }
}
