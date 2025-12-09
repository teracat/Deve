using Deve.Criteria;
using Deve.Data;
using Deve.DataSource.Config;
using Deve.External.Data;
using Deve.Internal.Criteria;
using Deve.Internal.Data;
using Deve.Internal.Model;
using Deve.Model;

namespace Deve.DataSource
{
    /// <summary>
    /// Methods to access some data out of the IData implementation which are available to the Core.
    /// It can inherit from IData, but is not a must.
    /// </summary>
    public interface IDataSource : IDisposable
    {
        IDataSourceConfig? Config { get; }
        IDataOptions Options { get; set; }

        IDataAll<Country, Country, CriteriaCountry> Countries { get; }
        IDataAll<State, State, CriteriaState> States { get; }
        IDataAll<City, City, CriteriaCity> Cities { get; }
        IDataClient Clients { get; }
        IDataAll<User, User, CriteriaUser> Users { get; }

        IDataGet<ClientBasic, External.Model.Client, CriteriaClientBasic> ClientsBasic { get; }
    }
}