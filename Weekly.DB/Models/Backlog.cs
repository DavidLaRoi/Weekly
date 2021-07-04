using System;
using System.Collections.Generic;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class Backlog
    {
        public Backlog()
        {
            BacklogItems = new HashSet<BacklogItem>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<BacklogItem> BacklogItems { get; set; }
    }
}
