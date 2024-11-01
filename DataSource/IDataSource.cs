using Deve.External;
using Deve.Internal;

namespace Deve.DataSource
{
    /// <summary>
    /// Methods to access some data out of the IData implementation which are available to the Core.
    /// It can inherit from IData, but is not a must.
    /// </summary>
    public interface IDataSource : IDisposable
    {
        DataSourceConfig? Config { get; }
        DataOptions Options { get; set; }

        IDataAll<Country, Country, CriteriaCountry> Countries { get; }
        IDataAll<State, State, CriteriaState> States { get; }
        IDataAll<City, City, CriteriaCity> Cities { get; }
        IDataClient Clients { get; }
        IDataAll<User, User, CriteriaUser> Users { get; }

        IDataGet<ClientBasic, External.Client, CriteriaClientBasic> ClientsBasic { get; }
    }
}
