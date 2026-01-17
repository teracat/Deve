using Microsoft.Extensions.DependencyInjection;
using Deve.Customers.Countries;

namespace Deve.Customers;

public static class CustomersDependencyInjection
{
    public static IServiceCollection AddModuleCustomers(this IServiceCollection services) => services.RegisterModule(typeof(Core).Assembly);

    public static IEndpointRouteBuilder MapEndpointsCustomers(this IEndpointRouteBuilder builder) => builder.MapEndpoints(typeof(Core).Assembly);
}
