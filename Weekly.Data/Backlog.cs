using System.Collections.Generic;

namespace Weekly.Data
{
    public class Backlog : DataItem
    {
        public Group Group { get; set; }

        public List<BacklogItem> Items { get; set; }
    }

}
