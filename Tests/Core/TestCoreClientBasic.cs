using Deve.Core;
using Deve.External;

namespace Deve.Tests.Core
{
    public class TestCoreClientBasic : TestCoreBaseDataGet<ClientBasic, Client, CriteriaClientBasic>
    {
        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> GetDataGet(ICore core) => core.ClientsBasic;
    }
}