using System;
using System.Collections.Generic;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class Group : UserEntity
    {
        public Group()
        {
            Tasks = new HashSet<Task>();
        }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
