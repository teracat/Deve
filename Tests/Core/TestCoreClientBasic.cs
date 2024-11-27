using Deve.Core;
using Deve.External;

namespace Deve.Tests.Core
{
    public class TestCoreClientBasic : TestBaseDataGet<ICore, ClientBasic, Client, CriteriaClientBasic>
    {
        protected override ICore CreateData() => TestsCoreHelpers.CreateCore();

        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> GetDataGet(ICore data) => data.ClientsBasic;
    }
}