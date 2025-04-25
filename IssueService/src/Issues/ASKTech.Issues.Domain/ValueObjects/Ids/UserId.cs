﻿using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.ValueObjects.Ids
{
    public class UserId : ComparableValueObject
    {
        private UserId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; init; }

        public static UserId NewUserId() => new(Guid.NewGuid());

        public static UserId Empty() => new(Guid.Empty);

        public static UserId Create(Guid id) => new(id);

        public static implicit operator UserId(Guid id) => new(id);

        public static implicit operator Guid(UserId userId)
        {
            ArgumentNullException.ThrowIfNull(userId);
            return userId.Value;
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }
    }
}
