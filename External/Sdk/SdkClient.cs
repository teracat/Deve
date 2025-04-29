using Deve.Model;
using Deve.Criteria;
using Deve.Sdk;
using Deve.External.Data;
using Deve.External.Model;

namespace Deve.External.Sdk
{
    internal class SdkClient : SdkBaseGet<ClientBasic, Client, CriteriaClientBasic, ISdkCommon>, IDataClientBasic
    {
        public SdkClient(ISdk sdk)
            : base(ApiConstants.PathClient, sdk)
        {
        }
    }
}
