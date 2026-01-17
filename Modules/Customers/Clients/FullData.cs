namespace Deve.Customers.Clients;

internal sealed record FullData(Client Client, City? City, State? State, Country? Country)
{
    public ClientListResponse ToListResponse() => new(
        Client.Id,
        Client.Name,
        Client.TradeName,
        Client.TaxId,
        City?.Name,
        State?.Name,
        Country?.Name,
        Client.Status,
        Client.Balance);

    public ClientResponse ToResponse() => new(
        Client.Id,
        Client.Name,
        Client.TradeName,
        Client.TaxId,
        Client.TaxName,
        City?.Id,
        City?.Name,
        State?.Id,
        State?.Name,
        Country?.Id,
        Country?.Name,
        Client.Status,
        Client.Balance);

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
