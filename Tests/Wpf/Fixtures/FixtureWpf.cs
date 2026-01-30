using Moq;
using Deve.Clients.Wpf.Interfaces;
using Deve.Data;
using Deve.Identity.Enums;
using Deve.Tests.Core;

namespace Deve.Tests.Wpf.Fixtures;

public class FixtureWpf : CommonFixture
{
    internal IData DataNoAuth { get; }
    internal IData DataAuthUser { get; }
    internal IData DataAuthAdmin { get; }

    internal Mock<IMessageHandler> MessageHandler { get; }
    internal Mock<INavigationService> NavigationService { get; }

    public FixtureWpf()
    {
        DataNoAuth = new MainTestsCore(Options, null);
        DataAuthUser = new MainTestsCore(Options, Role.User);
        DataAuthAdmin = new MainTestsCore(Options, Role.Admin);

        NavigationService = new Mock<INavigationService>();
        MessageHandler = new Mock<IMessageHandler>();
    }

    #region IAsyncLifetime
    public override Task DisposeAsync()
    {
        DataNoAuth.Dispose();
        DataAuthUser.Dispose();
        DataAuthAdmin.Dispose();
        return base.DisposeAsync();
    }
    #endregion
}
