using System;
using System.Collections.Generic;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class Task : Model
    {
        public Task()
        {
            TaskHasTaskChildTasks = new HashSet<TaskHasTask>();
            TaskHasTaskParentTasks = new HashSet<TaskHasTask>();
        }

        public TimeSpan? Duration { get; set; }
        public Guid? GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<TaskHasTask> TaskHasTaskChildTasks { get; set; }
        public virtual ICollection<TaskHasTask> TaskHasTaskParentTasks { get; set; }
    }
}
