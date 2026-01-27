using Deve.Data;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core;

public abstract class BaseDataGetCoreTest<TReponseList, TReponseGetById> : DataGetBaseTest<IData, TReponseList, TReponseGetById>, IClassFixture<CoreFixture>
{
    protected BaseDataGetCoreTest(CoreFixture fixtureDataCore)
        : base(fixtureDataCore)
    {
    }
}
