﻿using Elastic.CommonSchema;
using Elastic.CommonSchema.Serilog;
using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Log = Serilog.Log;

namespace ASKTech.Framework.Logging
{
    public static class LoggingExtensions
    {
        public static IServiceCollection AddApplicationLoggingElastic(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var elasticOptions = configuration.GetSection(ElasticOptions.ELASTIC)
                .Get<ElasticOptions>() ?? throw new ArgumentNullException(nameof(ElasticOptions));

            string indexFormat =
                $"{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:dd-MM-yyyy}";

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Elasticsearch(
                    [new Uri(elasticOptions.ElasticEndpoint)],
                    options =>
                    {
                        options.DataStream = new DataStreamName(indexFormat);
                        options.TextFormatting = new EcsTextFormatterConfiguration();
                        options.BootstrapMethod = BootstrapMethod.Silent;
                    })
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
                .CreateLogger();

            services.AddSerilog();

            return services;
        }

        public static IServiceCollection AddApplicationLoggingSeq(
            this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Seq(configuration.GetConnectionString("Seq")
                             ?? throw new ArgumentNullException("Seq"))
                .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
                .CreateLogger();

            services.AddSerilog();

            return services;
        }
    }
}
