﻿using ASKTech.Issues.Domain.Lesson;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASKTech.Issues.Domain.ValueObjects.Ids;
using ASKTech.Issues.Domain.ValueObjects;

namespace ASKTech.Issues.Infrastructure.Configurations.Write
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.ToTable("lessons");

            builder.HasKey(l => l.Id);

            builder.Property(i => i.Id)
                .HasConversion(
                    id => id.Value,
                    value => LessonId.Create(value));

            builder.Property(l => l.ModuleId)
                .IsRequired()
                .HasColumnName("module_id");

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

            builder.ComplexProperty(b => b.Experience, eb =>
            {
                eb.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("experience");
            });

            builder.Property(l => l.PreviewId)
                .IsRequired(false)
                .HasColumnName("preview_id");

            builder.Property(x => x.Tags)
                .HasColumnName("tags")
                .HasColumnType("uuid[]");

            builder.Property(x => x.Issues)
                .HasColumnName("issues")
                .HasColumnType("uuid[]");

            builder
                .ComplexProperty(l => l.Video, vb =>
                {
                    vb.Property(t => t.FileId)
                        .IsRequired()
                        .HasColumnName("file_id");

                    vb.Property(t => t.FileLocation)
                        .IsRequired()
                        .HasColumnName("file_location");
                });
        }
    }
}
