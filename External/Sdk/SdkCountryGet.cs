namespace Deve.External.Sdk
{
    internal class SdkCountryGet : SdkBaseGet<Country, Country, CriteriaCountry, ISdk>
    {
        #region Properties
        protected override string Path => ApiConstants.ApiPathCountry;
        #endregion

        #region Constructor
        public SdkCountryGet(ISdk sdk)
            : base(sdk)
        {
        }
        #endregion
    }
}
