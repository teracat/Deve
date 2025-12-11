using Deve.Dto;
using Deve.Sdk;

namespace Deve.Internal.Sdk
{
    internal class SdkBaseAll<ModelList, Model, Criteria> : External.Sdk.SdkBaseGet<ModelList, Model, Criteria, ISdk>
    {
        #region Constructor
        public SdkBaseAll(string path, ISdk sdk)
            : base(path, sdk)
        {
        }
        #endregion

        #region IDataAll Methods
        public async Task<ResultGet<ModelId>> Add(Model data) => await PostWithResult<ModelId>(Path, RequestAuthType.Default, data);

        public async Task<Result> Update(Model data) => await Put(Path, RequestAuthType.Default, data);

        public async Task<Result> Delete(long id) => await Delete(Path + id, RequestAuthType.Default);
        #endregion
    }
}