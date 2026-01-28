namespace Deve.Customers.Clients;

internal sealed record FullData(Client Client, City? City, State? State, Country? Country)
{
    public ClientListResponse ToListResponse() => new()
    {
        Id = Client.Id,
        Name = Client.Name,
        TradeName = Client.TradeName,
        TaxId = Client.TaxId,
        CityName = City?.Name,
        StateName = State?.Name,
        CountryName = Country?.Name,
        Status = Client.Status,
        Balance = Client.Balance
    };

    public ClientResponse ToResponse() => new()
    {
        Id = Client.Id,
        Name = Client.Name,
        TradeName = Client.TradeName,
        TaxId = Client.TaxId,
        TaxName = Client.TaxName,
        CityId = City?.Id,
        CityName = City?.Name,
        StateId = State?.Id,
        StateName = State?.Name,
        CountryId = Country?.Id,
        CountryName = Country?.Name,
        Status = Client.Status,
        Balance = Client.Balance
    };

    public static IQueryable<FullData> CreateQuery(IRepository<Client> repositoryClient, IRepository<City> repositoryCity, IRepository<State> repositoryState, IRepository<Country> repositoryCountry) =>
        repositoryClient.GetAsQueryable()
                        .LeftJoin(repositoryCity.GetAsQueryable(),
                                    client => client.CityId,
                                    city => city.Id,
                                    (client, city) => new { Client = client, City = city }
                        )
                        .LeftJoin(repositoryState.GetAsQueryable(),
                                    p => p.City == null ? Guid.Empty : p.City.StateId,
                                    state => state.Id,
                                    (p, state) => new { p.Client, p.City, State = state }
                        )
                        .LeftJoin(repositoryCountry.GetAsQueryable(),
                                    p => p.State == null ? Guid.Empty : p.State.CountryId,
                                    country => country.Id,
                                    (p, country) => new FullData(p.Client, p.City, p.State, country)
                        );
}
