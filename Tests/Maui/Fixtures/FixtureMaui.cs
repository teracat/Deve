using Moq;
using Deve.Clients.Maui.Interfaces;
using Deve.Data;
using Deve.Identity.Enums;
using Deve.Tests.Core;

namespace Deve.Tests.Maui.Fixtures;

public class FixtureMaui : CommonFixture
{
    internal IData DataNoAuth { get; }
    internal IData DataAuthUser { get; }
    internal IData DataAuthAdmin { get; }
    internal Mock<INavigationService> NavigationService { get; }

    public FixtureMaui()
    {
        DataNoAuth = new MainTestsCore(Options, null);
        DataAuthUser = new MainTestsCore(Options, Role.User);
        DataAuthAdmin = new MainTestsCore(Options, Role.Admin);

        NavigationService = new Mock<INavigationService>();
    }

    public override Task DisposeAsync()
    {
        DataNoAuth.Dispose();
        DataAuthUser.Dispose();
        DataAuthAdmin.Dispose();
        return Task.CompletedTask;
    }
}
