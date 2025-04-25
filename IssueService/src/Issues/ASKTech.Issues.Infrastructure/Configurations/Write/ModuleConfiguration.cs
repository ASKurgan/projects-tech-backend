﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASKTech.Issues.Domain.Module;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using ASKTech.Issues.Domain.ValueObjects;

namespace ASKTech.Issues.Infrastructure.Configurations.Write
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("modules");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasConversion(
                    id => id.Value,
                    value => ModuleId.Create(value));

            builder.ComplexProperty(m => m.Title, tb =>
            {
                tb.Property(t => t.Value)
                    .IsRequired()
                    .HasMaxLength(Title.MAX_LENGTH)
                    .HasColumnName("title");
            });

            builder.ComplexProperty(m => m.Description, tb =>
            {
                tb.Property(d => d.Value)
                    .IsRequired()
                    .HasMaxLength(Description.MAX_LENGTH)
                    .HasColumnName("description");
            });

            builder.HasMany(m => m.IssuesPosition)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(i => i.LessonsPosition)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(m => m.IsDeleted)
                .HasColumnName("is_deleted");

            builder.Property(m => m.DeletionDate)
                .IsRequired(false)
                .HasColumnName("deletion_date");

            builder.HasQueryFilter(f => f.IsDeleted == false);
        }
    }
}
