using Deve.Sdk;

namespace Deve.External.Sdk
{
    internal class SdkClientBasicGet : SdkBaseGet<ClientBasic, Client, CriteriaClientBasic, ISdkCommon>
    {
        #region Fields
        private string _path;
        #endregion

        #region Properties
        protected override string Path => _path;
        #endregion

        #region Constructor
        public SdkClientBasicGet(ISdkCommon sdk, string path)
            : base(sdk)
        {
            _path = path;
        }
        #endregion
    }
}
