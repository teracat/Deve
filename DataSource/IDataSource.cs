using Deve.Dto;
using Deve.Data;
using Deve.DataSource.Config;
using Deve.External.Data;
using Deve.Internal.Dto;
using Deve.Internal.Data;

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

        IDataGet<ClientBasic, External.Dto.Client, CriteriaClientBasic> ClientsBasic { get; }
    }
}