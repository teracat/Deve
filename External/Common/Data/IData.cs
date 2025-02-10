using Deve.Criteria;
using Deve.Data;
using Deve.Model;
using Deve.External.Model;

namespace Deve.External.Data
{
    public interface IData : IDataCommon
    {
        IDataGet<Country, Country, CriteriaCountry> Countries { get; }
        IDataGet<State, State, CriteriaState> States { get; }
        IDataGet<City, City, CriteriaCity> Cities { get; }
        IDataGet<ClientBasic, Client, CriteriaClientBasic> Clients { get; }
    }
}