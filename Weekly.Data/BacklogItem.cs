using System;
using System.Collections.Generic;

namespace Weekly.Data
{
    public class BacklogItem : DataItem
    {
        public DateTime? Completed { get; set; }

        public int Priority { get; set; }

        public List<BacklogItem> Items { get; set; }
    }

}
