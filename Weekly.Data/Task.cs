using System;
using System.Collections.Generic;

namespace Weekly.Data
{
    public class DataItem
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

    }

    public class Task : DataItem
    {
        public Group Group { get; set; }

        public List<Task> Tasks { get; set; }
    }

}
