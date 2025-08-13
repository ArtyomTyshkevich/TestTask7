using System;

namespace TestTask.Domain.Entities.Warehouse
{
    public class IncomingDocument
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public virtual List<IncomingResource> IncomingResources { get; set; } = new List<IncomingResource>();

    }
}
