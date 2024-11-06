namespace Deve.Internal
{
    public interface IDataAll<ModelList, Model, Criteria> : External.IDataGet<ModelList, Model, Criteria>
    {
        Task<ResultGet<ModelId>> Add(Model data);
        Task<Result> Update(Model data);
        Task<Result> Delete(long id);
    }
}
