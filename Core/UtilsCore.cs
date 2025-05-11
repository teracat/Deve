using Deve.Data;
using Deve.Model;

namespace Deve.Core
{
    internal static class UtilsCore
    {
        public static Result? CheckIdWhenAdding<Model>(IDataOptions options, Model data, IList<Model> list) where Model: ModelId
        {
            if (data.Id > 0)
            {
                if (list.Any(x => x.Id == data.Id))
                {
                    return Utils.ResultError(options.LangCode, ResultErrorType.DuplicatedValue, nameof(data.Id));
                }
            }
            else
            {
                //When adding, the Id is optional. If it's not received, we assign the Max value + 1
                data.Id = list.Max(x => x.Id) + 1;
            }
            return null;
        }
    }
}
