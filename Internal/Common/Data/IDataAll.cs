using Deve.Dto;

namespace Deve.Internal.Data
{
    public interface IDataAll<ModelList, Model, Criteria> : External.Data.IDataGet<ModelList, Model, Criteria>
    {
        Task<ResultGet<ModelId>> Add(Model data);
        Task<Result> Update(Model data);
        Task<Result> Delete(long id);
    }
}
