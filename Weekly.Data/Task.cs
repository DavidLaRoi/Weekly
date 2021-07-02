using System;
using System.Collections.Generic;

namespace Weekly.Data
{
    public class Task
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Group Group { get; set; }

        public List<Task> Tasks { get; set; }
    }

}
