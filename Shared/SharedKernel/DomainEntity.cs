using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public abstract class DomainEntity<TId> : Entity<TId>
     where TId : IComparable<TId>
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        protected DomainEntity(TId id)
            : base(id)
        {
        }

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void ClearDomainEvents() => _domainEvents.Clear();

        protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    }
}
