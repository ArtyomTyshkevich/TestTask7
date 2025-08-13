using System;
using TestTask.Domain.Entities.Directories;
using TestTask.Domain.Enums;

namespace TestTask.Domain.Entities.Warehouse
{
    public class OutgoingDocument
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public virtual Client Client { get; set; } = new Client();
        public DateTime Date { get; set; }
        public OutgoingStateEnum State { get; set; }
        public virtual List<OutgoingResource> OutgoingResources { get; set; } = new List<OutgoingResource>();
    }
}
