using Deve.Model;
using Deve.Sdk;
using Deve.External.Data;

namespace Deve.External.Sdk
{
    internal class SdkBaseGet<ModelList, Model, Criteria, SdkType> : SdkBase<SdkType>, IDataGet<ModelList, Model, Criteria> where SdkType : ISdkCommon
    {
        #region Atributes
        private readonly string _path;
        #endregion

        #region Properties
        protected string Path => _path;
        #endregion

        #region Constructor
        public SdkBaseGet(string path, SdkType sdk)
            : base(sdk)
        {
            _path = path;
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