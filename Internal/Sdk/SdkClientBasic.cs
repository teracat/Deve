using Deve.Criteria;
using Deve.External.Data;
using Deve.External.Sdk;
using Deve.Model;
using Deve.Sdk;

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
