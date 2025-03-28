using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; }

        public DateTime? DeletionDate { get; }

        public void SoftDelete();

        public void Restore();
    }
}
