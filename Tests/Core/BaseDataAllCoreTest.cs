using Deve.Data;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core;

public abstract class BaseDataAllCoreTest<TReponseList, TReponseGetById> : DataAllBaseTest<IData, TReponseList, TReponseGetById>, IClassFixture<CoreFixture>
{
    protected BaseDataAllCoreTest(CoreFixture fixtureDataCore)
        : base(fixtureDataCore)
    {
    }
}
