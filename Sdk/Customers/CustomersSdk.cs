using Deve.Customers;
using Deve.Customers.Cities;
using Deve.Customers.Clients;
using Deve.Customers.Countries;
using Deve.Customers.States;
using Deve.Customers.Stats;

namespace Deve.Sdk.Customers;

internal class CustomersSdk(ISdk sdk) : ICustomersData
{
    public ICountryData Countries => field ??= new CountrySdk(sdk);

    public IStateData States => field ??= new StateSdk(sdk);

    public ICityData Cities => field ??= new CitySdk(sdk);

    public IClientData Clients => field ??= new ClientSdk(sdk);

    public IStatsData Stats => field ??= new StatsSdk(sdk);
}
