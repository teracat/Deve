using Deve.Criteria;
using Deve.Internal.Data;
using Deve.Model;

namespace Deve.Internal.Sdk
{
    internal class SdkCity : SdkBaseAll<City, City, CriteriaCity>, IDataCity
    {
        public SdkCity(ISdk sdk)
            : base(ApiConstants.PathCity, sdk)
        {
        }
    }
}
