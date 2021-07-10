using System;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class Schedule
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
