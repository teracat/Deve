using Deve.Data;

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
