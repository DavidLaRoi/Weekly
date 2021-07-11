using System;
using System.Collections.Generic;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class TaskTask
    {
        public Guid Id { get; set; }
        public Guid ParentTaskId { get; set; }
        public Guid ChildTaskId { get; set; }
        public int Rank { get; set; }

        public virtual Task ChildTask { get; set; }
        public virtual Task ParentTask { get; set; }
    }
}
