using System;
using System.Collections.Generic;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class Task
    {
        public Task()
        {
            TaskHasTaskParentTasks = new HashSet<TaskHasTask>();
            TaskHasTaskSubTasks = new HashSet<TaskHasTask>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TaskHasTask> TaskHasTaskParentTasks { get; set; }
        public virtual ICollection<TaskHasTask> TaskHasTaskSubTasks { get; set; }
    }
}
