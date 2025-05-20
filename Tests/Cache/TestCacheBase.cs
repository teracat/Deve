using Deve.Tests.Cache.Fixtures;

namespace Cache
{
    public abstract class TestCacheBase
    {
        private readonly IFixtureCache _fixtureCache;

        protected TestCacheBase(IFixtureCache fixtureCache)
        {
            _fixtureCache = fixtureCache;
        }

        #region Tests
        [Fact]
        public void Get_NotExists_ReturnsFalse()
        {
            var result = _fixtureCache.Cache.TryGet("NotExistingKey", out string value);

            Assert.False(result);
        }

        [Fact]
        public void Set_NewKey_NoException()
        {
            var exception = Record.Exception(() => _fixtureCache.Cache.Set("NewKey", "Value"));

            Assert.Null(exception);
        }
        #endregion
    }
}
