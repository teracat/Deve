using Deve.Dto;
using Deve.External.Data;

namespace Deve.External.Sdk
{
    internal class SdkCity : SdkBaseGet<City, City, CriteriaCity, ISdk>, IDataCity
    {
        public SdkCity(ISdk sdk)
            : base(ApiConstants.PathCity, sdk)
        {
        }
    }
}
