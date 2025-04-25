using CSharpFunctionalExtensions;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.ValueObjects
{
    public class SocialNetwork : ComparableValueObject
    {
        public SocialNetwork(string name, string link)
        {
            Name = name;
            Link = link;
        }

        public string Name { get; }

        public string Link { get; }

        public static Result<SocialNetwork, Error> Create(string link, string name)
        {
            if (string.IsNullOrWhiteSpace(link))
                return Errors.General.ValueIsRequired("link");

            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValueIsRequired("name");

            return new SocialNetwork(link, name);
        }

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Name;
            yield return Link;
        }
    }
}
