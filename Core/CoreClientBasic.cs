using Deve.Auth;
using Deve.Auth.Permissions;
using Deve.Auth.UserIdentityService;
using Deve.Cache;
using Deve.Dto;
using Deve.Data;
using Deve.DataSource;
using Deve.External.Data;
using Deve.External.Dto;

namespace Deve.Core
{
    public class CoreClientBasic : CoreBaseGet<ClientBasic, Client, CriteriaClientBasic>, IDataClientBasic
    {
        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> DataGet => Source.ClientsBasic;
        protected override PermissionDataType DataType => PermissionDataType.ClientBasic;

        public CoreClientBasic(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentityService userIdentityService, ICache cache)
            : base(dataSource, auth, options, userIdentityService, cache)
        {
        }
    }
}
