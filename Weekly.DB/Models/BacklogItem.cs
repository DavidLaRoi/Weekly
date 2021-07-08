using System;
using System.Collections.Generic;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class BacklogItem
    {
        public BacklogItem()
        {
            BiHasBiChildren = new HashSet<BiHasBi>();
            BiHasBiParents = new HashSet<BiHasBi>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? PriorityId { get; set; }
        public DateTime? CompletedOn { get; set; }
        public Guid? BacklogId { get; set; }
        public TimeSpan? Duration { get; set; }

        public virtual Backlog Backlog { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual ICollection<BiHasBi> BiHasBiChildren { get; set; }
        public virtual ICollection<BiHasBi> BiHasBiParents { get; set; }
    }
}
