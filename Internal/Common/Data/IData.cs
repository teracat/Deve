using Deve.Criteria;
using Deve.Data;
using Deve.Model;
using Deve.Internal.Criteria;
using Deve.External.Data;
using Deve.Internal.Model;

namespace Deve.Internal.Data
{
    /// <summary>
    /// For Internal Use
    /// </summary>
    public interface IData : IDataCommon
    {
        IDataAll<Country, Country, CriteriaCountry> Countries { get; }
        IDataAll<State, State, CriteriaState> States { get; }
        IDataAll<City, City, CriteriaCity> Cities { get; }
        IDataClient Clients { get; }
        IDataGet<ClientBasic, External.Model.Client, CriteriaClientBasic> ClientsBasic { get; }
        IDataAll<UserBase, UserPlainPassword, CriteriaUser> Users { get; }
        IDataStats Stats { get; }
    }
}