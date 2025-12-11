using Deve.Dto;

namespace Deve.External.Data
{
    public interface IDataGet<ModelList, Model, Criteria>
    {
        Task<ResultGetList<ModelList>> Get(Criteria? criteria);

        Task<ResultGetList<ModelList>> Get();

        Task<ResultGet<Model>> Get(long id);
    }
}
