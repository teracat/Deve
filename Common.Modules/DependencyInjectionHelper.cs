using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Deve.Hash;
using Deve.Abstractions.Endpoints;
using Deve.Repositories;
using Deve.Shield;
using Deve.Behaviors;
using Deve.Abstractions.Handlers;

namespace Deve;

public static class DependencyInjectionHelper
{
    public static IServiceCollection Register(this IServiceCollection services, Assembly assembly, Type subtype, ServiceLifetime lifetime)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var typesToRegister = assembly.GetTypes()
                                      .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(subtype));
        foreach (var type in typesToRegister)
        {
            var interfaces = type.GetInterfaces().ToArray();
            foreach (var iface in interfaces)
            {
                _ = lifetime switch
                {
                    ServiceLifetime.Transient => services.AddTransient(iface, type),
                    ServiceLifetime.Scoped => services.AddScoped(iface, type),
                    ServiceLifetime.Singleton => services.AddSingleton(iface, type),
                    _ => services.AddScoped(iface, type),
                };
            }
        }
        return services;
    }

    public static IServiceCollection RegisterModule(this IServiceCollection services, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        _ = Register(services,
                     assembly,
                     typeof(IFeature),
                     ServiceLifetime.Scoped);

        _ = Register(services,
                     assembly,
                     typeof(IModule),
                     ServiceLifetime.Scoped);

        _ = Register(services,
                     assembly,
                     typeof(IRepository),
                     ServiceLifetime.Scoped);

        _ = Register(services,
                     assembly,
                     typeof(IRequestHandler),
                     ServiceLifetime.Transient);

        _ = Register(services,
                     assembly,
                     typeof(IValidator),
                     ServiceLifetime.Transient);

        _ = Register(services,
                     assembly,
                     typeof(IPipelineBehavior),
                     ServiceLifetime.Transient);

        _ = Register(services,
                     assembly,
                     typeof(INotificationHandler),
                     ServiceLifetime.Transient);

        return services;
    }

    public static IServiceCollection AddCommon(this IServiceCollection services) =>
        services.AddSingleton<IShield, ShieldMain>()
                .AddSingleton<IHash, HashSha512>()
                .AddScoped<IMediator, InMemoryMediator>()
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app, Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly);

        var endpoints = assembly
            .DefinedTypes
            .Where(t => t.IsClass && !t.IsAbstract && t.IsAssignableTo(typeof(IEndpoint)))
            .Select(t => Activator.CreateInstance(t) as IEndpoint);
        foreach (var endpoint in endpoints)
        {
            endpoint?.MapEndpoint(app);
        }
        return app;
    }
}
