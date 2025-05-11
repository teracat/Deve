using Deve.Model;
using Deve.Criteria;
using Deve.Sdk;
using Deve.External.Sdk;
using Deve.External.Data;

namespace Deve.Internal.Sdk
{
    internal class SdkClientBasic : SdkBaseGet<ClientBasic, External.Model.Client, CriteriaClientBasic, ISdkCommon>, IDataClientBasic
    {
        public SdkClientBasic(ISdk sdk)
            : base(ApiConstants.PathClientBasic, sdk)
        {
        }
    }
}
