﻿using Deve.Internal.Model;
using Deve.Model;

namespace Deve.DataSource
{
    internal static class Data
    {
        //Countries
        public static readonly List<Country> Countries =
        [
            new Country() { Id = 1, Name = "España", IsoCode = "ES" },
            new Country() { Id = 2, Name = "USA", IsoCode = "US" },
        ];

        //States
        public static readonly List<State> States =
        [
            new State() { Id = 1, Name = "Barcelona", CountryId = 1, Country = "España" },
            new State() { Id = 2, Name = "Washington", CountryId = 2, Country = "USA" },
        ];

        //Cities
        public static readonly List<City> Cities =
        [
            new City() { Id = 1, Name = "Santpedor", StateId = 1, State = "Barcelona", CountryId = 1, Country = "España" },
            new City() { Id = 2, Name = "Barcelona", StateId = 1, State = "Barcelona", CountryId = 1, Country = "España" },
            new City() { Id = 3, Name = "Washington DC", StateId = 2, State = "Washington", CountryId = 2, Country = "USA" },
            new City() { Id = 4, Name = "Redmond", StateId = 2, State = "Washington", CountryId = 2, Country = "USA" },
        ];

        //Clients
        public static readonly List<Client> Clients =
        [
            new Client() { Id = 1, Name = "Jordi Badia", TradeName = "Teracat", Balance = 50, Status = ClientStatus.Active, TaxName = "Jordi Badia Santaulària", Location = new Location()
            {
                CityId = 1,
                City = "Santpedor",
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
        ];

        //Users
        public static readonly List<User> Users =
        [
            new User() { Id = 1, Role = Role.Admin, Name = "Jordi Badia", Username = "teracat", Email = "jordib@teracat.com", IsActive = true, Joined = new DateTime(2024, 9, 19), PasswordHash = "Rke4hgrN46Zgzeivca85NdtghI7CL3yFtbzoLTxHe0HYsEUscTRH8TtJw1Si6z+/krDvdzSBoCLWs55slzOV3Q==" },
            new User() { Id = 2, Role = Role.User, Name = "Dan Brown", Username = "dan.brown",  IsActive = false, Joined = new DateTime(2024, 9, 19), PasswordHash = "9jn9NVRbBwRdo0/+5c63F6pO77Jzc8Der3nH8vyDiHjunLrFefqlkbf55TF7SS+LhCrDj20bt77LxPetqLaYWA==" },
        ];
    }
}