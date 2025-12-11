using Deve.Dto;
using Deve.Internal.Data;

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
