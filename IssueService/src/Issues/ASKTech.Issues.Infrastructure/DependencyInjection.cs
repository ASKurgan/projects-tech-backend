using ASKTech.Core.Database;
using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Infrastructure.DbContexts;
using ASKTech.Issues.Infrastructure.Migrator;
using ASKTech.Issues.Infrastructure.Outbox;
using ASKTech.Issues.Infrastructure.Repositories;
using ASKTech.Issues.Infrastructure.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContexts(configuration)
                .AddRepositories()
                .AddDatabase()
                .AddHostedServices()
                .AddServices()
                .AddQuartzService()
                .AddMessageBus(configuration)
                .AddMigrators();

            return services;
        }

        private static IServiceCollection AddMigrators(this IServiceCollection services)
        {
            services.AddScoped<IMigrator, IssuesMigrator>();

            return services;
        }

        private static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit<IIssueMessageBus>(configure =>
            {
                configure.SetKebabCaseEndpointNameFormatter();

                configure.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(configuration["RabbitMQ:Host"]!), h =>
                    {
                        h.Username(configuration["RabbitMQ:UserName"]!);
                        h.Password(configuration["RabbitMQ:Password"]!);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            return services;
        }

        private static IServiceCollection AddQuartzService(this IServiceCollection services)
        {
            services.AddScoped<ProcessOutboxMessagesService>();

            services.AddQuartz(configure =>
            {
                var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

                configure
                    .AddJob<ProcessOutboxMessagesJob>(jobKey)
                    .AddTrigger(trigger => trigger.ForJob(jobKey).WithSimpleSchedule(
                        schedule => schedule.WithIntervalInSeconds(1).RepeatForever()));
            });

            services.AddQuartzHostedService(options => { options.WaitForJobsToComplete = true; });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILessonsRepository, LessonsRepository>();
            services.AddScoped<IModulesRepository, ModulesRepository>();
            services.AddScoped<IIssuesReviewRepository, IssuesReviewRepository>();
            services.AddScoped<IUserIssueRepository, UserIssueRepository>();
            services.AddScoped<IModulesRepository, ModulesRepository>();
            services.AddScoped<IIssuesRepository, IssuesesRepository>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();

            return services;
        }

        private static IServiceCollection AddDbContexts(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            //services.AddScoped<IssuesDbContext>(provider =>
            //    new IssuesDbContext(configuration.GetConnectionString("Database")!));

            services.AddScoped<IIssuesReadDbContext, IssuesDbContext>(provider =>
                new IssuesDbContext(configuration.GetConnectionString("Database")!));

            return services;
        }

        private static IServiceCollection AddHostedServices(this IServiceCollection services)
        {
            // services.AddHostedService<DeleteExpiredIssuesBackgroundService>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<DeleteExpiredIssuesService>();

            return services;
        }
    }
}
