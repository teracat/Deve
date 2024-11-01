namespace Deve.External
{
    public interface IData : IDataCommon
    {
        IDataGet<Country, Country, CriteriaCountry> Countries { get; }
        IDataGet<State, State, CriteriaState> States { get; }
        IDataGet<City, City, CriteriaCity> Cities { get; }
        IDataGet<ClientBasic, Client, CriteriaClientBasic> Clients { get; }
    }
}
