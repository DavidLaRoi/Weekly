using System;
using System.Collections.Generic;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class Priority
    {
        public Priority()
        {
            BacklogItems = new HashSet<BacklogItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }

        public virtual ICollection<BacklogItem> BacklogItems { get; set; }
    }
}
