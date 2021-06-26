using System;
using System.Collections.Generic;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class TaskHasTask
    {
        public Guid ParentTaskId { get; set; }
        public Guid SubTaskId { get; set; }
        public int Index { get; set; }

        public virtual Task ParentTask { get; set; }
        public virtual Task SubTask { get; set; }
    }
}
