using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
 using OpenTelemetry.Instrumentation;
using Microsoft.Kiota.Authentication.Azure;
using Npgsql;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Framework.Observability
{
    public static class ObservabilityExtensions
    {
        public static IServiceCollection AddObservability(
            this IServiceCollection services,
            IConfiguration configuration,
            string[] meterNames,
            string[] sourceNames)
        {
            var observabilityOptions = configuration.GetSection(ObservabilityOptions.OBSERVABILITY)
                .Get<ObservabilityOptions>() ?? throw new ArgumentNullException(nameof(ObservabilityOptions));

            services.AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(observabilityOptions.ServiceName))
                .WithMetrics(metrics => metrics
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(observabilityOptions.ServiceName))
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    //.AddProcessInstrumentation()
                    //.AddPrometheusExporter()
                    //.AddMeter(meterNames)
                    )
                .WithTracing(tracing => tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddNpgsql()
                    .AddSource(sourceNames)
                    .AddOtlpExporter(c => c.Endpoint = new Uri(observabilityOptions.OltpEndpoint)));

            return services;
        }

        
        
            //
            // Сводка:
            //     Enables process instrumentation.
            //
            // Параметры:
            //   builder:
            //     OpenTelemetry.Metrics.MeterProviderBuilder being configured.
            //
            // Возврат:
            //     The instance of OpenTelemetry.Metrics.MeterProviderBuilder to chain the calls.
            //public static MeterProviderBuilder AddProcessInstrumentation(this MeterProviderBuilder builder)
            //{
            //    OpenTelemetry.Internal.Guard.ThrowIfNull(builder, "builder");
            //    builder.AddMeter(ProcessMetrics.MeterName);
            //    return builder.AddInstrumentation(() => new ProcessMetrics());
            //}
        
    }
}
