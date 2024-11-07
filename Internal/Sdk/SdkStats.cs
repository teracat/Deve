using Deve.Sdk;

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
            return await Get<ClientStats>(ApiConstants.ApiPathStats + ApiConstants.ApiMethodGetClientStats, RequestAuthType.Default);
        }
        #endregion
    }
}
