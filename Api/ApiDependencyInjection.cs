using Deve.Modules;
// <hooks:api-di-using>
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
                   // <hooks:api-di-addmodule>
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
            // <hooks:api-di-mapendpoints>
            .MapEndpointsAuth()
            .MapEndpointsIdentity()
            .MapEndpointsCustomers();
        return app;
    }
}
