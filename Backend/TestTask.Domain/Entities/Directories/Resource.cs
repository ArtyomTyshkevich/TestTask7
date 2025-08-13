using System;
using TestTask.Domain.Enums;

namespace TestTask.Domain.Entities.Directories
{
    public class Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DirectoriesStateEnum State { get; set; }
    }
}
