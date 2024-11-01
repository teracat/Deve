namespace Deve.Internal
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
        External.IDataGet<ClientBasic, External.Client, CriteriaClientBasic> ClientsBasic { get; }
        IDataAll<User, User, CriteriaUser> Users { get; }
        IDataStats Stats { get; }
    }
}
