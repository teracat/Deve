using Deve.Sdk;

namespace Deve.External.Sdk
{
    internal class SdkCityGet : SdkBaseGet<City, City, CriteriaCity, ISdk>
    {
        #region Properties
        protected override string Path => ApiConstants.ApiPathCity;
        #endregion

        #region Constructor
        public SdkCityGet(ISdk sdk)
            : base(sdk)
        {
        }
        #endregion
    }
}
