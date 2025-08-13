using TestTask.Application.DTOs.Directories;

namespace TestTask.Application.DTOs.Warehouse
{
    public class OutgoingResourceDTO
    {
        public Guid? Id { get; set; }
        public ResourceDTO ResourceDTO { get; set; } = new ResourceDTO();
        public UnitDTO UnitDTO { get; set; } = new UnitDTO();
        public decimal Quantity { get; set; }
    }
}
