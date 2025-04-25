using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.IssueSolving.ValueObjects
{
    public class Attempts : ComparableValueObject
    {
        private Attempts(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public Attempts Add() => Create(Value + 1);

        public static Attempts Create(int attempts = 1)
        {
            return new Attempts(attempts);
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }
    }
}
