using Moq;
using Deve.Auth.UserIdentityService;
using Deve.Auth;
using Deve.Identity.Enums;

namespace Deve.Tests.Mocks.IdentityService;

public class AdminAuthUserIdentityServiceMock : Mock<IUserIdentityService>
{
    public AdminAuthUserIdentityServiceMock()
    {
        _ = SetupGet(d => d.UserIdentity).Returns(new UserIdentity(TestsConstants.ValidUserId, TestsConstants.UserUsernameValid, Role.Admin));
        _ = SetupSet(d => d.UserIdentity = It.IsAny<UserIdentity?>());
        _ = SetupGet(d => d.IsAuthenticated).Returns(true);
        _ = SetupGet(d => d.IsAdmin).Returns(true);
    }
}
