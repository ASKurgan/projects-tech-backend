﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Core.Abstractions
{
    public static class HandlersExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classes => classes
                    .AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan.FromAssemblies(assemblies)
                .AddClasses(classes => classes
                    .AssignableToAny(typeof(IQueryHandler<,>), typeof(IQueryHandlerWithResult<,>)))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

            return services;
        }
    }
}
