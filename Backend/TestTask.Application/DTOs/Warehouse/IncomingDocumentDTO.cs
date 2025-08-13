using System;
using TestTask.Domain.Entities.Warehouse;

namespace TestTask.Application.DTOs.Warehouse
{
    public class IncomingDocumentDTO
    {
        public Guid? Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public List<IncomingResourceDTO> IncomingResourcesDTO { get; set; } = new List<IncomingResourceDTO>();
    }
}
