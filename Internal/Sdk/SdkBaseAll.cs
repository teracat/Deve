using Deve.Model;
using Deve.Sdk;
using Deve.Internal.Data;

namespace Deve.Internal.Sdk
{
    internal class SdkBaseAll<ModelList, Model, Criteria> : External.Sdk.SdkBaseGet<ModelList, Model, Criteria, ISdk>, IDataAll<ModelList, Model, Criteria>
    {
        #region Constructor
        public SdkBaseAll(string path, ISdk sdk)
            : base(path, sdk)
        {
        }
        #endregion

        #region IDataAll Methods
        public async Task<ResultGet<ModelId>> Add(Model data)
        {
            return await PostWithResult<ModelId>(Path, RequestAuthType.Default, data);
        }

        public async Task<Result> Update(Model data)
        {
            return await Put(Path, RequestAuthType.Default, data);
        }

        public async Task<Result> Delete(long id)
        {
            return await Delete(Path + id, RequestAuthType.Default);
        }
        #endregion
    }
}