using System;
using TestTask.Application.DTOs.Directories;
using TestTask.Domain.Entities.Directories;

namespace TestTask.Application.DTOs.Warehouse
{
    public class BalanceDTO
    {
        public Guid Id { get; set; }
        public ResourceDTO ResourceDTO { get; set; } = new ResourceDTO();
        public UnitDTO UnitDTO { get; set; } = new UnitDTO();
        public decimal Quantity { get; set; }
    }
}
