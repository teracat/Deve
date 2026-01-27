using Deve.Cache;

namespace Deve.Tests.Cache.Fixtures;

public interface ICacheFixture
{
    ICache Cache { get; }
}
