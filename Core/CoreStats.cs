using Deve.Model;
using Deve.Auth;
using Deve.Auth.Permissions;
using Deve.Auth.UserIdentityService;
using Deve.Internal.Data;
using Deve.Internal.Model;
using Deve.Data;
using Deve.DataSource;
using Deve.Cache;

namespace Deve.Core
{
    public class CoreStats : CoreBase, IDataStats
    {
        #region Properties
        protected ICache Cache { get; }
        #endregion

        #region Constructor
        public CoreStats(IDataSource dataSource, IAuth auth, IDataOptions options, IUserIdentityService userIdentityService, ICache cache)
            : base(dataSource, auth, options, userIdentityService)
        {
            Cache = cache;
        }
        #endregion

        #region Methods
        public async Task<ResultGet<ClientStats>> GetClientStats()
        {
            var resPerm = await CheckPermission(PermissionType.Get, PermissionDataType.Stats);
            if (!resPerm.Success)
            {
                return Utils.ResultGetError<ClientStats>(resPerm);
            }

            // Example of using cache: if the item is already in the cache, return it directly.
            if (Cache.TryGet(nameof(ClientStats), out ClientStats value))
            {
                return Utils.ResultGetOk(value);
            }

            var clients = await Source.Clients.Get();
            var result = new ClientStats
            {
                MinBalance = clients.Data.Min(x => x.Balance),
                MaxBalance = clients.Data.Max(x => x.Balance),
                AvgBalance = clients.Data.Average(x => x.Balance),
            };

            // If the item is not in the cache, add it.
            Cache.Set(nameof(ClientStats), result);

            return Utils.ResultGetOk(result);
        }
        #endregion
    }
}
