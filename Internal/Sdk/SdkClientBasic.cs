using Deve.Dto;
using Deve.External.Data;
using Deve.External.Sdk;
using Deve.Sdk;

namespace Deve.Internal.Sdk
{
    internal class SdkClientBasic : SdkBaseGet<ClientBasic, External.Dto.Client, CriteriaClientBasic, ISdkCommon>, IDataClientBasic
    {
        public SdkClientBasic(ISdk sdk)
            : base(ApiConstants.PathClientBasic, sdk)
        {
        }
    }
}
