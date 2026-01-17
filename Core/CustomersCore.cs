using Deve.Customers;
using Deve.Customers.Cities;
using Deve.Customers.Clients;
using Deve.Customers.Countries;
using Deve.Customers.States;
using Deve.Customers.Stats;

namespace Deve.Core;

internal sealed class CustomersCore(
    ICountryData countries,
    IStateData states,
    ICityData city,
    IClientData client,
    IStatsData stats) : ICustomersData
{
    public ICountryData Countries => countries;

    public IStateData States => states;

    public ICityData Cities => city;

    public IClientData Clients => client;

    public IStatsData Stats => stats;
}
