namespace Deve.Customers.States;

internal sealed record FullData(State State, Country? Country)
{
    public StateResponse ToResponse() => new(State.Id, State.Name, State.CountryId, Country?.Name);

    public static IQueryable<FullData> CreateQuery(IRepository<State> repositoryState, IRepository<Country> repositoryCountry) =>
        repositoryState.GetAsQueryable()
                       .LeftJoin(repositoryCountry.GetAsQueryable(),
                                 state => state.CountryId,
                                 country => country.Id,
                                 (state, country) => new FullData(state, country)
                       );
}
