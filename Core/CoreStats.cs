using Deve.Model;
using Deve.Auth.Permissions;
using Deve.Internal.Data;
using Deve.Internal.Model;

namespace Deve.Core
{
    internal class CoreStats : CoreBase, IDataStats
    {
        #region Constructor
        public CoreStats(CoreMain core)
            : base(core)
        {
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

            var clients = await Source.Clients.Get();
            var result = new ClientStats
            {
                MinBalance = clients.Data.Min(x => x.Balance),
                MaxBalance = clients.Data.Max(x => x.Balance),
                AvgBalance = clients.Data.Average(x => x.Balance),
            };

            return Utils.ResultGetOk(result);
        }
        #endregion
    }
}