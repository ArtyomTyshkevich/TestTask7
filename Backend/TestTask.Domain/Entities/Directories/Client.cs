using System;
using TestTask.Domain.Enums;

namespace TestTask.Domain.Entities.Directories
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DirectoriesStateEnum State { get; set; }
    }
}
