using Deve.Criteria;
using Deve.Data;
using Deve.Model;
using Deve.External.Model;

namespace Deve.External.Data
{
    public interface IData : IDataCommon
    {
        IDataCountry Countries { get; }
        IDataState States { get; }
        IDataCity Cities { get; }
        IDataClientBasic Clients { get; }
    }
}
