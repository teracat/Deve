using Deve.Model;
using Deve.Sdk;
using Deve.Internal.Data;
using Deve.Internal.Model;

namespace Deve.Internal.Sdk
{
    internal class SdkStats : SdkBase<ISdk>, IDataStats
    {
        #region Constructor
        public SdkStats(ISdk sdk)
            : base(sdk)
        {
        }
        #endregion

        #region IDataStats
        public async Task<ResultGet<ClientStats>> GetClientStats()
        {
            return await Get<ClientStats>(ApiConstants.PathStats + ApiConstants.MethodGetClientStats, RequestAuthType.Default);
        }
        #endregion
    }
}