using Deve.Dto;
using Deve.External.Data;
using Deve.External.Dto;
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
