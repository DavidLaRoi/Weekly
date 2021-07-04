using System;
using System.Collections.Generic;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class Group : Model
    {
        public Group()
        {
            Backlogs = new HashSet<Backlog>();
            Tasks = new HashSet<Task>();
        }

        public virtual ICollection<Backlog> Backlogs { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
