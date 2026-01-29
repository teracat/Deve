using Moq;
using Deve.Auth.UserIdentityService;
using Deve.Auth;

namespace Deve.Tests.Mocks.IdentityService;

public class NoAuthUserIdentityServiceMock : Mock<IUserIdentityService>
{
    public NoAuthUserIdentityServiceMock()
    {
        _ = SetupGet(d => d.UserIdentity).Returns((UserIdentity?)null);
        _ = SetupSet(d => d.UserIdentity = It.IsAny<UserIdentity?>());
        _ = SetupGet(d => d.IsAuthenticated).Returns(false);
        _ = SetupGet(d => d.IsAdmin).Returns(false);
    }
}
