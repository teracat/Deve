using Microsoft.Extensions.DependencyInjection;

namespace Deve.Customers;

public static class CustomersDependencyInjection
{
    public static IServiceCollection AddModuleCustomers(this IServiceCollection services) => services.RegisterModule(typeof(CustomersDependencyInjection).Assembly);

    public static IEndpointRouteBuilder MapEndpointsCustomers(this IEndpointRouteBuilder builder) => builder.MapEndpoints(typeof(CustomersDependencyInjection).Assembly);
}
