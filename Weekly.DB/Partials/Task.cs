using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
#nullable disable

namespace Weekly.DB.Models
{

    public partial class Task
    {
        [NotMapped]
        public IEnumerable<Task> Parents => TaskHasTaskSubTasks.Select((x) => x.ParentTask);

        [NotMapped]
        public IEnumerable<Task> Children => TaskHasTaskParentTasks.Select((x) => x.SubTask);

        public override string ToString()
        {
            return Name;
        }
    }
}
