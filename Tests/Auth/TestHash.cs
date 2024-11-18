using Deve.Auth;

namespace Deve.Tests.Auth
{
    /// <summary>
    /// Hash Tests.
    /// </summary>
    public class TestHash
    {
        [Fact]
        public void Calc_Null_ReturnsNull()
        {
            var auth = AuthFactory.Get();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var hash = auth.Hash.Calc(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            Assert.Null(hash);
        }

        [Fact]
        public void Calc_Empty_ReturnsEmpty()
        {
            var auth = AuthFactory.Get();

            var hash = auth.Hash.Calc(string.Empty);

            Assert.Empty(hash);
        }

        [Fact]
        public void Calc_Valid_Equal()
        {
            var auth = AuthFactory.Get();
            
            var hash = auth.Hash.Calc("Original Text");
            System.Diagnostics.Debug.WriteLine(hash);

            Assert.Equal("JhlKGi1fOIFe2j81JzmaUtRPVoVaYcmq+Ulzpwe4rR9gxfyrHoFUtNFZnNh4y4nPQX/my0nFbcKsLyNSbi5NHQ==", hash);
        }
    }
}
