using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
#nullable disable

namespace Weekly.DB.Models
{

    public partial class Task
    {
        [NotMapped]
        public IEnumerable<Task> ParentTasks => TaskHasTaskParentTasks.Select((x) => x.ParentTask);

        [NotMapped]
        public IEnumerable<Task> Children => TaskHasTaskSubTasks.Select((x) => x.SubTask);

    }
}
