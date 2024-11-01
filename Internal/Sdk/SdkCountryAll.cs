using Deve.External.Sdk;

namespace Deve.Internal.Sdk
{
    internal class SdkCountryAll : SdkBaseAll<Country, Country, CriteriaCountry>
    {
        #region Properties
        protected override string Path => ApiConstants.ApiPathCountry;
        #endregion

        #region Constructor
        public SdkCountryAll(ISdk sdk)
            : base(sdk)
        {
        }
        #endregion
    }
}
