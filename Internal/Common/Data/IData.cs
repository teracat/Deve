using Deve.Data;
using Deve.External.Data;

namespace Deve.Internal.Data
{
    /// <summary>
    /// For Internal Use
    /// </summary>
    public interface IData : IDataCommon
    {
        IDataCountry Countries { get; }
        IDataState States { get; }
        IDataCity Cities { get; }
        IDataClient Clients { get; }
        IDataClientBasic ClientsBasic { get; }
        IDataUser Users { get; }
        IDataStats Stats { get; }
    }
}
