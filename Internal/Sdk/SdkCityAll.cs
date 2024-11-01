using Deve.External.Sdk;

namespace Deve.Internal.Sdk
{
    internal class SdkCityAll : SdkBaseAll<City, City, CriteriaCity>
    {
        #region Properties
        protected override string Path => ApiConstants.ApiPathCity;
        #endregion

        #region Constructor
        public SdkCityAll(ISdk sdk)
            : base(sdk)
        {
        }
        #endregion
    }
}
