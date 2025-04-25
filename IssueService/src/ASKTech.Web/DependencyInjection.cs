using ASKTech.Core.Abstractions;
using ASKTech.Core.Caching;
using ASKTech.Framework.Authorization;
using ASKTech.Framework.Logging;
using ASKTech.Framework.Observability;
using ASKTech.Framework.Swagger;
using ASKTech.Issues.Application;
using ASKTech.Issues.Infrastructure;
using FileService.Communication;
using FluentValidation;
using MassTransit.Logging;
using MassTransit.Monitoring;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ASKTech.Web
{
    public static class DependencyInjection
    {
        public static void AddProgramDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationLoggingSeq(configuration);

            var assemblies = new[]
            {
                typeof(ASKTech.Issues.Application.DependencyInjection).Assembly,
            };

            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services
                .AddApplication()
                .AddInfrastructure(configuration)
                .AddFramework(configuration, assemblies);
        }

        private static IServiceCollection AddFramework(
            this IServiceCollection services,
            IConfiguration configuration,
            params Assembly[] assemblies)
        {
            services.AddEndpointsApiExplorer()
                .AddValidatorsFromAssemblies(assemblies)
                .AddHandlers(assemblies)
                .AddCustomSwagger(configuration)
                .AddAuthServices(configuration)
                .AddDistributedCache(configuration)
                .AddObservability(configuration, [InstrumentationOptions.MeterName],
                    [DiagnosticHeaders.DefaultListenerName]);

            services.AddFileHttpCommunication(configuration);

            services.AddHttpContextAccessor()
                .AddScoped<UserScopedData>();

            return services;
        }
    }
}
