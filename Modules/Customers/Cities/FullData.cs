namespace Deve.Customers.Cities;

internal sealed record FullData(City City, State? State, Country? Country)
{
    public CityResponse ToResponse() => new(City.Id, City.Name, City.StateId, State?.Name, Country?.Name);

    public static IQueryable<FullData> CreateQuery(IRepository<City> repositoryCity, IRepository<State> repositoryState, IRepository<Country> repositoryCountry) =>
        repositoryCity.GetAsQueryable()
                      .LeftJoin(repositoryState.GetAsQueryable(),
                                city => city.StateId,
                                state => state.Id,
                                (city, state) => new { City = city, State = state }
                      )
                      .LeftJoin(repositoryCountry.GetAsQueryable(),
                                p => p.State == null ? Guid.Empty : p.State.CountryId,
                                country => country.Id,
                                (p, country) => new FullData(p.City, p.State, country)
                      );
}
