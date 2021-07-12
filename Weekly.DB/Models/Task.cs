using System;
using System.Collections.Generic;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class Task
    {
        public Task()
        {
            TaskTaskChildTasks = new HashSet<TaskTask>();
            TaskTaskParentTasks = new HashSet<TaskTask>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan? Duration { get; set; }
        public Guid? GroupId { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<TaskTask> TaskTaskChildTasks { get; set; }
        public virtual ICollection<TaskTask> TaskTaskParentTasks { get; set; }
    }
}
