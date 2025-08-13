using System;
using TestTask.Domain.Enums;

namespace TestTask.Application.DTOs.Directories
{
    public class UnitDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public DirectoriesStateEnum State { get; set; }
    }
}
