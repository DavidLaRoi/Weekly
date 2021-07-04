using System;

namespace Weekly.Data
{
    public class BacklogItem : DataItem
    {
        public DateTime? Completed { get; set; }

        public int Priority { get; set; }
    }

}
