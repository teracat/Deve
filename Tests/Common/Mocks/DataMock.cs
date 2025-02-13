using Deve.Model;
using Deve.Internal.Model;

namespace Deve.Tests.Mocks
{
    /// <summary>
    /// Data used in the DataSourceMock.
    /// Id 1 will be used as the data to Get by Id (check the virtual property defined in Tests.TestBaseDataGet.ValidId).
    /// Id 1 will also be used to test Update methods (check the overriden method CreateValidDataToUpdate in every Tests.TestXXX).
    /// Id 3 will be used to test Delete methods (check the virtual property defined in Tests.TestBaseDataAll.ValidIdDelete).
    /// </summary>
    public static class DataMock
    {
        // Countries
        public static readonly List<Country> Countries =
        [
            new Country() { Id = 1, Name = "España", IsoCode = "ES" },
            new Country() { Id = 2, Name = "USA", IsoCode = "US" },
            new Country() { Id = 3, Name = "France", IsoCode = "FR" },
        ];

        // States
        public static readonly List<State> States =
        [
            new State() { Id = 1, Name = "Barcelona", CountryId = 1, Country = "España" },
            new State() { Id = 2, Name = "Washington", CountryId = 2, Country = "USA" },
            new State() { Id = 3, Name = "Madrid", CountryId = 1, Country = "España" },
        ];

        // Cities
        public static readonly List<City> Cities =
        [
            new City() { Id = 1, Name = "Santpedor", StateId = 1, State = "Barcelona", CountryId = 1, Country = "España" },
            new City() { Id = 2, Name = "Barcelona", StateId = 1, State = "Barcelona", CountryId = 1, Country = "España" },
            new City() { Id = 3, Name = "Washington DC", StateId = 2, State = "Washington", CountryId = 2, Country = "USA" },
            new City() { Id = 4, Name = "Redmond", StateId = 2, State = "Washington", CountryId = 2, Country = "USA" },
        ];

        // Clients
        // Client with Id 1 will also be used to test the UpdateStatus method.
        public static readonly List<Client> Clients =
        [
            new Client() { Id = 1, Name = "Jordi Badia", TradeName = "Teracat", Balance = 50, Status = ClientStatus.Active, TaxName = "Jordi Badia Santaulària", Location = new Location()
            {
                CityId = 1,
                City = "Santedor",
                CountryId = 1,
                Country = "España",
                StateId = 1,
                State = "Barcelona",
            }},
            new Client() { Id = 2, Name = "Microsoft", TradeName = "Microsoft", Balance = 1000, Status = ClientStatus.Inactive, TaxName = "Microsoft Corporation", Location = new Location()
            {
                CityId = 4,
                City = "Redmond",
                CountryId = 2,
                Country = "USA",
                StateId = 2,
                State = "Washington",
            }},
            new Client() { Id = 3, Name = "Fake Company", TradeName = "Fake", Balance = 500, Status = ClientStatus.Active, TaxName = "Fake Corporation", Location = new Location()
            {
                CityId = 1,
                City = "Santedor",
                CountryId = 1,
                Country = "España",
                StateId = 1,
                State = "Barcelona",
            }},
        ];

        // Users
        // User with Username "tests" will be used as the user to perform valid logins (it must have the Admin role to pass the permissions checks and it also must be active).
        // User with Username "tests2" will be used for inactive user tests.
        public static readonly List<User> Users =
        [
            new User() { Id = 1, Role = Role.Admin, Name = "Valid Tests User", Username = "tests", IsActive = true, Joined = new DateTime(2024, 9, 19), PasswordHash = "7iaw3Ur350mqGo7jwQrpkj9hiYB3Lkc/iBml1JQODbJ6wYX4oOHV+E+IvIh/1nsUNzLDBMxfqa2Ob1f1ACio/w==" },
            new User() { Id = 2, Role = Role.User, Name = "Inactive Tests User", Username = "tests2",  IsActive = false, Joined = new DateTime(2024, 9, 19), PasswordHash = "vaZLjDLI1HmTSVpN6AJEmpxgQdch6uHpMhZOcKLBPLHxkBEDEnds6hfkZCP4/g/UcbTqiZ1vgJBvzX3w3WWOtg==" },
            new User() { Id = 3, Role = Role.User, Name = "Fake User", Username = "fake",  IsActive = false, Joined = new DateTime(2024, 11, 19), PasswordHash = "9jn9NVRbBwRdo0/+5c63F6pO77Jzc8Der3nH8vyDiHjunLrFefqlkbf55TF7SS+LhCrDj20bt77LxPetqLaYWA==" },
        ];
    }
}