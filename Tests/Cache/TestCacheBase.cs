using Deve.Tests.Cache.Fixtures;

namespace Deve.Tests.Cache
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
            var result = _fixtureCache.Cache.TryGet("NotExistingKey", out string _);

            Assert.False(result);
        }

        [Fact]
        public void Set_NewKey_NoException()
        {
            var exception = Record.Exception(() => _fixtureCache.Cache.Set("NewKey", "Value"));

            Assert.Null(exception);
        }

        [Fact]
        public void SetAndGet_Value_FindsIt()
        {
            _fixtureCache.Cache.Set("NewExistingKey", "Value");
            var result = _fixtureCache.Cache.TryGet("NewExistingKey", out string _);

            Assert.True(result);
        }

        [Fact]
        public void SetAndGet_Value_SameValue()
        {
            _fixtureCache.Cache.Set("NewExistingKey2", "Value");
            _fixtureCache.Cache.TryGet("NewExistingKey2", out string value);

            Assert.Equal("Value", value);
        }
        #endregion
    }
}
