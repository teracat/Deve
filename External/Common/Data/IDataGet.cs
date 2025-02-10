using Deve.Model;

namespace Deve.External.Data
{
    public interface IDataGet<ModelList, Model, Criteria>
    {
        Task<ResultGetList<ModelList>> Get(Criteria? criteria = default);
        Task<ResultGet<Model>> Get(long id);
    }
}
