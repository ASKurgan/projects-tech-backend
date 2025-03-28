﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ASKTech.Core.Database
{
    public static class EfCoreFluentApiExtensions
    {
        public static PropertyBuilder<IReadOnlyList<TValueObject>> ValueObjectsCollectionJsonConversion<TValueObject>(
            this PropertyBuilder<IReadOnlyList<TValueObject>> builder)
        {
            return builder.HasConversion(
                    valueObjects => JsonSerializer.Serialize(valueObjects, JsonSerializerOptions.Default),
                    json => JsonSerializer.Deserialize<IReadOnlyList<TValueObject>>(json, JsonSerializerOptions.Default)!,
                    CreateCollectionValueComparer<TValueObject>())
                .HasColumnType("jsonb");
        }

        public static PropertyBuilder<IReadOnlyList<TValueObject>> ValueObjectsCollectionJsonConversion<TValueObject, TDto>(
            this PropertyBuilder<IReadOnlyList<TValueObject>> builder,
            Func<TValueObject, TDto> toDtoSelector,
            Func<TDto, TValueObject> toValueObjectSelector)
        {
            return builder.HasConversion(
                    valueObjects => SerializeValueObjectsCollection(valueObjects, toDtoSelector),
                    json => DeserializeDtoCollection(json, toValueObjectSelector),
                    CreateCollectionValueComparer<TValueObject>())
                .HasColumnType("jsonb");
        }

        public static string SerializeValueObjectsCollection() =>
            JsonSerializer.Serialize(string.Empty, JsonSerializerOptions.Default);

        public static IEnumerable<TDto> DeserializeDtoCollection<TDto>(string json)
        {
            return JsonSerializer.Deserialize<IEnumerable<TDto>>(json, JsonSerializerOptions.Default) ?? [];
        }

        private static string SerializeValueObjectsCollection<TValueObject, TDto>(
            IReadOnlyList<TValueObject> valueObjects, Func<TValueObject, TDto> selector)
        {
            var dtos = valueObjects.Select(selector);

            return JsonSerializer.Serialize(dtos, JsonSerializerOptions.Default);
        }

        private static IReadOnlyList<TValueObject> DeserializeDtoCollection<TValueObject, TDto>(
            string json, Func<TDto, TValueObject> selector)
        {
            var dtos = JsonSerializer.Deserialize<IEnumerable<TDto>>(json, JsonSerializerOptions.Default) ?? [];

            return dtos.Select(selector).ToList();
        }

        private static ValueComparer<IReadOnlyList<T>> CreateCollectionValueComparer<T>() =>
            new(
                (c1, c2) => c1!.SequenceEqual(c2!),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
                c => c.ToList());
    }
}
