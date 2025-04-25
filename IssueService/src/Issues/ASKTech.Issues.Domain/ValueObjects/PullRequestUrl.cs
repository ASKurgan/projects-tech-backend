﻿using CSharpFunctionalExtensions;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.ValueObjects
{
    public class PullRequestUrl : ComparableValueObject
    {
        private const string PATTERN = @"^https:\/\/github\.com\/[^\/]+\/[^\/]+\/pull\/\d+$";

        public PullRequestUrl(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<PullRequestUrl, Error> Create(string pullRequestUrl)
        {
            if (!Regex.IsMatch(pullRequestUrl, PATTERN))
                return Errors.General.ValueIsInvalid("pull request url");

            return new PullRequestUrl(pullRequestUrl);
        }

        public static readonly PullRequestUrl Empty = new PullRequestUrl(string.Empty);

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }
    }
}
