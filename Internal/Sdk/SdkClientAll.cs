using Deve.Sdk;

namespace Deve.Internal.Sdk
{
    internal class SdkClientAll : SdkBaseAll<Client, Client, CriteriaClient>, IDataClient
    {
        #region Properties
        protected override string Path => ApiConstants.ApiPathClient;
        #endregion

        #region Constructor
        public SdkClientAll(ISdk sdk)
            : base(sdk)
        {
        }
        #endregion

        #region IDataClient
        public async Task<Result> UpdateStatus(long id, ClientStatus newStatus)
        {
            return await Put(Path + id + "/" + ((int)newStatus).ToString(), RequestAuthType.Default, null);
        }
        #endregion
    }
}