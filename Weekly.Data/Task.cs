﻿using System.Collections.Generic;

namespace Weekly.Data
{


    public class Task : DataItem
    {
        public Group Group { get; set; }

        public List<Task> Tasks { get; set; }
    }

}
