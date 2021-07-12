using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
#nullable disable

namespace Weekly.DB.Models
{

    public partial class Task : Models.Entity
    {
        [NotMapped]
        public IEnumerable<Task> Parents => TaskTaskParentTasks.Select((x) => x.ParentTask);

        [NotMapped]
        public IEnumerable<Task> Children => TaskTaskChildTasks.Select((x) => x.ChildTask);

        public override string ToString()
        {
            return Name;
        }
    }

    public class Entity
    {
        public Guid Id { get; set; }
    }

    public class UserEntity : Entity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
