using Deve.Auth.TokenManagers;
using Deve.Data;
using Deve.Auth;
using Deve.Identity.Enums;
using Deve.Sdk;
using Deve.Tests.Api.Fixture;

namespace Deve.Tests.Sdk.Fixtures;

public class SdkFixture : FixtureApi, IDataFixture<IData>
{
    public ITokenManager TokenManager { get; }
    public IData DataNoAuth { get; }
    public IData DataAuthUser { get; }
    public IData DataAuthAdmin { get; }

    public SdkFixture()
    {
        TokenManager = TestsHelpers.CreateTokenManager();
        DataNoAuth = SdkBuilder.Create(CreateClient());
        DataAuthUser = CreateSdk(Role.User);
        DataAuthAdmin = CreateSdk(Role.Admin);
    }

    private ISdk CreateSdk(Role role)
    {
        var sdk = SdkBuilder.Create(CreateClient());
        var userIdentity = new UserIdentity(TestsConstants.ValidUserId, TestsConstants.UserUsernameValid, role);
        sdk.UserToken = TokenManager.CreateToken(userIdentity);
        return sdk;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            DataNoAuth?.Dispose();
            DataAuthUser?.Dispose();
            DataAuthAdmin?.Dispose();
            TokenManager.Dispose();
        }
        base.Dispose(disposing);
    }
}
