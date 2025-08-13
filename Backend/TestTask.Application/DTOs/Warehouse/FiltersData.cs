using System;
using TestTask.Application.DTOs.Directories;


namespace TestTask.Application.DTOs.Warehouse
{
    public class FiltersData
    {
        public List<UnitDTO>? unitDTOs {  get; set; }  = new List<UnitDTO>();
        public List<ResourceDTO>? ResourceDTOs { get; set; } = new List<ResourceDTO>();
        public List<ClientDTO>? ClientDTOs { get; set; } = new List<ClientDTO>();
        public List<IncomingDocumentDTO>? incomingDocumentDTOs { get; set; } = new List<IncomingDocumentDTO>();
        public List<OutgoingDocumentDTO>? outgoingDocumentDTOs { get; set; } = new List<OutgoingDocumentDTO>();
    }
}
