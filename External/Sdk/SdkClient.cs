using Deve.Criteria;
using Deve.External.Data;
using Deve.External.Model;
using Deve.Model;
using Deve.Sdk;

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
