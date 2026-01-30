using Deve.Tests.Auth.Fixtures;

namespace Deve.Tests.Auth;

/// <summary>
/// Hash Tests.
/// </summary>
public class HashTest : IClassFixture<AuthFixture>
{
    private readonly AuthFixture _fixtureAuth;

    public HashTest(AuthFixture authFixture)
    {
        _fixtureAuth = authFixture;
    }

    [Fact]
    public void Calc_Null_ReturnsNull()
    {
        var hash = _fixtureAuth.Hash.Calc(null);

        Assert.Null(hash);
    }

    [Fact]
    public void Calc_Empty_ReturnsEmpty()
    {
        var hash = _fixtureAuth.Hash.Calc(string.Empty);

        Assert.Empty(hash);
    }

    [Fact]
    public void Calc_Valid_Equal()
    {
        var hash = _fixtureAuth.Hash.Calc("Original Text");
        System.Diagnostics.Debug.WriteLine(hash);

        Assert.Equal("JhlKGi1fOIFe2j81JzmaUtRPVoVaYcmq+Ulzpwe4rR9gxfyrHoFUtNFZnNh4y4nPQX/my0nFbcKsLyNSbi5NHQ==", hash);
    }
}
