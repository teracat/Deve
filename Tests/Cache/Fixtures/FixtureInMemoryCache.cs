using Deve.Cache;

namespace Deve.Tests.Cache.Fixtures
{
    public class FixtureInMemoryCache : IFixtureCache
    {
        public ICache Cache { get; }

        public FixtureInMemoryCache()
        {
            Cache = new InMemoryCache();
        }
    }
}
