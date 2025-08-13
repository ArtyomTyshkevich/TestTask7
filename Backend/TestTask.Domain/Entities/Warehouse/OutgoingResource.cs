using System;
using TestTask.Domain.Entities.Directories;

namespace TestTask.Domain.Entities.Warehouse
{
    public class OutgoingResource
    {
        public Guid Id { get; set; }
        public virtual Resource Resource { get; set; } = new Resource();
        public virtual Unit Unit { get; set; } = new Unit();
        public decimal Quantity { get; set; }

    }
}
