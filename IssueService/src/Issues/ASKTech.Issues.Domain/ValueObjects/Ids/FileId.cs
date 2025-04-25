using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASKTech.Issues.Domain.ValueObjects.Ids
{
    public class FileId : ComparableValueObject
    {
        private FileId(Guid value)
        {
            Value = value;
        }

        public FileId()
        {
        }

        public Guid Value { get; }

        public static FileId NewFileId() => new(Guid.NewGuid());

        public static FileId Empty() => new(Guid.Empty);

        public static FileId Create(Guid id) => new(id);

        protected override IEnumerable<IComparable> GetComparableEqualityComponents()
        {
            yield return Value;
        }
    }
}
