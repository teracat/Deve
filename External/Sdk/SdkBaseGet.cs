using Deve.Sdk;

namespace Deve.External.Sdk
{
    internal abstract class SdkBaseGet<ModelList, Model, Criteria, SdkType> : SdkBase<SdkType>, IDataGet<ModelList, Model, Criteria> where SdkType : ISdkCommon
    {
        #region Constructor
        public SdkBaseGet(SdkType sdk)
            : base(sdk)
        {
        }
        #endregion

        #region IDataGet Methods
        public async Task<ResultGetList<ModelList>> Get(Criteria? criteria = default)
        {
            return await GetList<ModelList, Criteria>(Path, criteria, RequestAuthType.Default);
        }

        public async Task<ResultGet<Model>> Get(long id)
        {
            return await Get<Model>(Path, RequestAuthType.Default, id);
        }
        #endregion
    }
}
