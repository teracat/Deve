using Deve.Model;
using Deve.Criteria;
using Deve.Auth.Permissions;
using Deve.External.Model;
using Deve.External.Data;
using Deve.Auth;
using Deve.Data;
using Deve.DataSource;

namespace Deve.Core
{
    public class CoreClientBasic : CoreBaseGet<ClientBasic, Client, CriteriaClientBasic>, IDataClientBasic
    {
        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> DataGet => Source.ClientsBasic;
        protected override PermissionDataType DataType => PermissionDataType.ClientBasic;

        public CoreClientBasic(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentity? userIdentity)
            : base(dataSource, auth, options, userIdentity)
        {
        }
    }
}
