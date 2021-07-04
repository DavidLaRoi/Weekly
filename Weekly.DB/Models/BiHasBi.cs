using System;
using System.Collections.Generic;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class BiHasBi
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public Guid ChildId { get; set; }
        public int Rank { get; set; }

        public virtual BacklogItem Child { get; set; }
        public virtual BacklogItem Parent { get; set; }
    }
}
