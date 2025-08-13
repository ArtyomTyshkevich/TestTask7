using TestTask.Application.DTOs.Directories;
using TestTask.Domain.Enums;

namespace TestTask.Application.DTOs.Warehouse
{
    public class OutgoingDocumentDTO
    {
        public Guid? Id { get; set; }
        public string Number { get; set; }
        public ClientDTO ClientDTO { get; set; } = new ClientDTO();
        public DateTime Date { get; set; }
        public DirectoriesStateEnum State { get; set; }
        public List<OutgoingResourceDTO> OutgoingResourcesDTO { get; set; } = new List<OutgoingResourceDTO>();
    }
}
