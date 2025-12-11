using Deve.External.Data;
using Deve.Dto;
using Deve.Sdk;

namespace Deve.External.Sdk
{
    internal class SdkBaseGet<ModelList, Model, Criteria, SdkType> : SdkBase<SdkType>, IDataGet<ModelList, Model, Criteria> where SdkType : ISdkCommon
    {
        #region Properties
        protected string Path { get; }
        #endregion

        #region Constructor
        public SdkBaseGet(string path, SdkType sdk)
            : base(sdk)
        {
            Path = path;
        }
        #endregion

        #region IDataGet Methods
        public async Task<ResultGetList<ModelList>> Get(Criteria? criteria) => await GetList<ModelList, Criteria>(Path, criteria, RequestAuthType.Default);

        public async Task<ResultGetList<ModelList>> Get() => await Get(default(Criteria));

        public async Task<ResultGet<Model>> Get(long id) => await Get<Model>(Path, RequestAuthType.Default, id);
        #endregion
    }
}