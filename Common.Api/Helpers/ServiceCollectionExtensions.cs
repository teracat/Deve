using System.Reflection;
using Deve.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Deve.Api.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterCoreClasses(this IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(CoreBase));
            if (assembly is not null)
            {
                var typesToRegister = assembly.GetTypes()
                                              .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(CoreBase)));
                foreach (var type in typesToRegister)
                {
                    var interfaces = type.GetInterfaces().ToArray();
                    foreach (var iface in interfaces)
                    {
                        _ = services.AddScoped(iface, type);
                    }
                }
            }

            return services;
        }
    }
}
