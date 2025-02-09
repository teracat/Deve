using Deve.Model;
using Deve.Criteria;
using Deve.Auth.Permissions;
using Deve.External.Model;
using Deve.External.Data;

namespace Deve.Core
{
    internal class CoreClientBasic : CoreBaseGet<ClientBasic, Client, CriteriaClientBasic>
    {
        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> DataGet => Source.ClientsBasic;
        protected override PermissionDataType DataType => PermissionDataType.ClientBasic;

        public CoreClientBasic(CoreMain core)
            : base(core)
        {
        }
    }
}