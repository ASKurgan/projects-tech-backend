﻿using CSharpFunctionalExtensions;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.Issue.ValueObjects
{
    public class Experience : ComparableValueObject
    {
        private const int MAX_VALUE = 1000;
        public int Value { get; }

        private Experience(int value)
        {
            Value = value;
        }

        public static Result<Experience, Error> Create(int experience)
        {
            if (experience is < 0 or > MAX_VALUE)
                return Errors.General.ValueIsInvalid("Experience");
            return new Experience(experience);
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }
    }
}
