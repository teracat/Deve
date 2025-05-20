using Deve.Cache;

namespace Deve.Tests.Cache.Fixtures
{
    public interface IFixtureCache
    {
        ICache Cache { get; }
    }
}
