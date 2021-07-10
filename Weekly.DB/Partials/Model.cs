using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Weekly.DB.Models
{
    public class Model
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
