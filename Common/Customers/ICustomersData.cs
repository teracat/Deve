// <hooks:data-using>
using Deve.Customers.Countries;
using Deve.Customers.States;
using Deve.Customers.Stats;
using Deve.Customers.Cities;
using Deve.Customers.Clients;

namespace Deve.Customers;

public interface ICustomersData : IModule
{
    // <hooks:data-properties>

    ICountryData Countries { get; }

    IStateData States { get; }

    ICityData Cities { get; }

    IClientData Clients { get; }

    IStatsData Stats { get; }
}
