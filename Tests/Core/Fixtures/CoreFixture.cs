using Deve.Data;
using Deve.Identity.Enums;

namespace Deve.Tests.Core.Fixtures;

public class CoreFixture : CommonFixture, IDataFixture<IData>
{
    public IData DataNoAuth { get; }
    public IData DataAuthUser { get; }
    public IData DataAuthAdmin { get; }

    public CoreFixture()
    {
        DataNoAuth = new MainTestsCore(Options, null);
        DataAuthUser = new MainTestsCore(Options, Role.User);
        DataAuthAdmin = new MainTestsCore(Options, Role.Admin);
    }

    public override Task DisposeAsync()
    {
        DataNoAuth.Dispose();
        DataAuthUser.Dispose();
        DataAuthAdmin.Dispose();
        return base.DisposeAsync();
    }
}
