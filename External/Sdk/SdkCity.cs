using Deve.Criteria;
using Deve.External.Data;
using Deve.Model;

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
