﻿using ASKTech.Core.Database;
using ASKTech.Framework.Middlewares;
using Serilog;

namespace ASKTech.Web.Extensions
{
   public static class WebApplicationExtensions
    {
        public static async Task Configure(this WebApplication app)
        {
            if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
            {
                await app.Services.RunMigrations();
                await app.Services.RunAutoSeeding();

               // app.UseOpenTelemetryPrometheusScrapingEndpoint();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseExceptionMiddleware();
            app.UseSerilogRequestLogging();
            app.ConfigureCors();
            app.UseAuthentication();
            app.UseScopeDataMiddleware();
            app.UseAuthorization();
            app.MapControllers();

            if (app.Environment.IsEnvironment("Docker"))
            {
                app.MapGet("/", () => "Hello World!");
            }
        }

        private static void ConfigureCors(this WebApplication app)
        {
            app.UseCors(config =>
            {
                config.WithOrigins("http://localhost:5173")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        }
    }
}
