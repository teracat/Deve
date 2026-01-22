// <hooks:core-using>
using Deve.Customers.Cities;
using Deve.Customers.Clients;
using Deve.Customers.Countries;
using Deve.Customers.States;
using Deve.Customers.Stats;

namespace Deve.Customers;

internal sealed class Core(
    // <hooks:core-contructor>
    ICountryData dataCountry,
    IStateData dataState,
    ICityData dataCity,
    IClientData dataClient,
    IStatsData dataStats) : ICustomersData
{
    // <hooks:core-properties>

    public ICountryData Countries => dataCountry;

    public IStateData States => dataState;

    public ICityData Cities => dataCity;

    public IClientData Clients => dataClient;

    public IStatsData Stats => dataStats;
}
