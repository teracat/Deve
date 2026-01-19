using Deve.Modules;
using Deve.Auth;
using Deve.Customers;
using Deve.Identity;

namespace Deve.Api;

internal static class ApiDependencyInjection
{
    public static ApiBuilder AddModules(this ApiBuilder builder)
    {
        _ = builder.Services
                   .AddCommon()
                   .AddModuleIdentity()
                   .AddModuleAuth()
                   .AddModuleCustomers();
        return builder;
    }

    public static WebApplication MapEndpointsModules(this WebApplication app)
    {
        _ = app
            .MapGroup("/")
            .AddEndpointFilter<ResultResponseFilter>()
            .MapEndpointsAuth()
            .MapEndpointsIdentity()
            .MapEndpointsCustomers();
        return app;
    }
}
