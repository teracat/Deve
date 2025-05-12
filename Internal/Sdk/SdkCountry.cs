using Deve.Model;
using Deve.Criteria;
using Deve.Internal.Data;

namespace Deve.Internal.Sdk
{
    internal class SdkCountry : SdkBaseAll<Country, Country, CriteriaCountry>, IDataCountry
    {
        public SdkCountry(ISdk sdk)
            : base(ApiConstants.PathCountry, sdk)
        {
        }
    }
}
