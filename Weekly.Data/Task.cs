using System;
using System.Collections.Generic;

namespace Weekly.Data
{
    public class Task : UserDto
    {
        public Group Group { get; set; }

        public TimeSpan? Duration { get; set; }

        public List<Task> Tasks { get; set; }
    }

}
