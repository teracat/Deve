using Deve.Customers.Enums;

namespace Deve.Customers;

internal static class Data
{
    //Countries
    private static readonly Guid SpainCountryId = Guid.Parse("e1f1a61b-66b2-4a64-84a6-373b88d031a3");
    private static readonly Guid UsaCountryId = Guid.Parse("aa40d411-9b5f-41ea-9f5d-c8d2a10ba606");

    public static readonly List<Country> Countries =
    [
        new Country() { Id = SpainCountryId, Name = "España", IsoCode = "ES" },
        new Country() { Id = UsaCountryId, Name = "USA", IsoCode = "US" },
    ];

    //States
    private static readonly Guid BarcelonaStateId = Guid.Parse("02034d02-e3b6-46e2-9b6c-3af25a7fc201");
    private static readonly Guid WashingtonStateId = Guid.Parse("b1c3f4d5-5f6a-4e7b-8c9d-0a1b2c3d4e5f");

    public static readonly List<State> States =
    [
        new State() { Id = BarcelonaStateId, Name = "Barcelona", CountryId = SpainCountryId },
        new State() { Id = WashingtonStateId, Name = "Washington", CountryId = UsaCountryId },
    ];

    //Cities
    private static readonly Guid SantpedorCityId = Guid.Parse("c1d2e3f4-5678-90ab-cdef-1234567890ab");
    private static readonly Guid BarcelonaCityId = Guid.Parse("d4e5f6a7-8901-2345-6789-0abcdef12345");
    private static readonly Guid WashingtonDCCityId = Guid.Parse("e5f6a7b8-9012-3456-7890-1bcdef234567");
    private static readonly Guid RedmondCityId = Guid.Parse("f6a7b8c9-0123-4567-8901-2cdef3456789");

    public static readonly List<City> Cities =
    [
        new City() { Id = SantpedorCityId, Name = "Santpedor", StateId = BarcelonaStateId,  },
        new City() { Id = BarcelonaCityId, Name = "Barcelona", StateId = BarcelonaStateId },
        new City() { Id = WashingtonDCCityId, Name = "Washington DC", StateId = WashingtonStateId },
        new City() { Id = RedmondCityId, Name = "Redmond", StateId = WashingtonStateId },
    ];

    //Clients
    private static readonly Guid TeracatClientId = Guid.Parse("1972b1b5-7360-4506-9d0a-059bdf85c8e1");
    private static readonly Guid MicrosoftClientId = Guid.Parse("2a3b4c5d-6e7f-8901-2345-67890abcdef1");

    public static readonly List<Client> Clients =
    [
        new Client() { Id = TeracatClientId, Name = "Jordi Badia", TradeName = "Teracat", Balance = 50, Status = ClientStatus.Active, TaxName = "Jordi Badia Santaulària", CityId = SantpedorCityId },
        new Client() { Id = MicrosoftClientId, Name = "Microsoft", TradeName = "Microsoft", Balance = 1000, Status = ClientStatus.Inactive, TaxName = "Microsoft Corporation", CityId = RedmondCityId },
    ];
}
