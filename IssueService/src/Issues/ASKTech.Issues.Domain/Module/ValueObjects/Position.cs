﻿using CSharpFunctionalExtensions;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.Module.ValueObjects
{
    public class Position : ComparableValueObject
    {
        public static readonly Position First = new(1);

        [JsonConstructor]
        private Position(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public Result<Position, Error> Forward()
            => Create(Value + 1);

        public Result<Position, Error> Back()
            => Create(Value - 1);

        public static implicit operator int(Position position) => position.Value;

        public static Result<Position, Error> Create(int number)
        {
            if (number < 1)
                return Errors.General.ValueIsInvalid("serial number");

            return new Position(number);
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }
    }
}
