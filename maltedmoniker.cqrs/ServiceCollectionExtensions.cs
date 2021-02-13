using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace maltedmoniker.cqrs
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDispatching(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddDispatcher();
            foreach(var assembly in assemblies)
            {
                services.AddHandlers(assembly);
            }
            return services;
        }

        public static IServiceCollection AddDispatching(this IServiceCollection services, Assembly assembly)
            => services
                .AddDispatcher()
                .AddHandlers(assembly);
        
        private static IServiceCollection AddDispatcher(this IServiceCollection services)
            => services.AddScoped<IDispatcher, Dispatcher>();

        private static IServiceCollection AddHandlers(this IServiceCollection services, Assembly assembly)
            => services
                .LoadGenericsFromAssemblyOfType(assembly, typeof(IHandler<,>))
                .LoadGenericsFromAssemblyOfType(assembly, typeof(IHandlerSync<,>))
                .LoadGenericsFromAssemblyOfType(assembly, typeof(IHandler<>))
                .LoadGenericsFromAssemblyOfType(assembly, typeof(IHandlerSync<>));

        private static IServiceCollection LoadGenericsFromAssemblyOfType(this IServiceCollection services, Assembly assembly, Type type)
        {
            foreach(var t in assembly.DefinedTypes.GetAbstractInterfacedMatchingTypes(type))
            {
                services.AddScoped(t.GetInterfaces()[0], t);
            }
            return services;
        }

        private static IEnumerable<TypeInfo> GetAbstractInterfacedMatchingTypes(this IEnumerable<TypeInfo> types, Type type)
            => types
                .Where(a =>
                    !a.IsAbstract
                    && a.GetInterfaces()
                        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == type))
                .ToList();
        
    }
}
