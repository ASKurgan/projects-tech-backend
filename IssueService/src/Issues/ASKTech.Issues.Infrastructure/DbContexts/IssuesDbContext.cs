using ASKTech.Issues.Application.Interfaces;
using ASKTech.Issues.Domain.Issue;
using ASKTech.Issues.Domain.IssueSolving.Entities;
using ASKTech.Issues.Domain.IssuesReviews;
using ASKTech.Issues.Domain.Lesson;
using ASKTech.Issues.Domain.Module;
using ASKTech.Issues.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Infrastructure.DbContexts
{
    public class IssuesDbContext : DbContext, IIssuesReadDbContext
    {
        private readonly string _connectionString;

        public IssuesDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Issue> Issues => Set<Issue>();

        public DbSet<Module> Modules => Set<Module>();

        public DbSet<UserIssue> UserIssues => Set<UserIssue>();

        public DbSet<IssueReview> IssueReviews => Set<IssueReview>();

        public DbSet<Lesson> Lessons => Set<Lesson>();

        public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();


        public IQueryable<Module> ReadModules => Set<Module>().AsQueryable().AsNoTracking();

        public IQueryable<Issue> ReadIssues => Set<Issue>().AsQueryable().AsNoTracking();

        public IQueryable<UserIssue> ReadUserIssues => Set<UserIssue>().AsQueryable().AsNoTracking();

        public IQueryable<IssueReview> ReadIssueReviews => Set<IssueReview>().AsQueryable().AsNoTracking();

        public IQueryable<Lesson> ReadLessons => Set<Lesson>().AsQueryable().AsNoTracking();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("issues");
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(IssuesDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Write") ?? false);
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder => { builder.AddConsole(); });
    }
}
