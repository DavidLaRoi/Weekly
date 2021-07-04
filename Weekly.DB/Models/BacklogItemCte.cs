using System;
using System.Collections.Generic;

#nullable disable

namespace Weekly.DB.Models
{
    public partial class BacklogItemCte
    {
        public Guid? RootId { get; set; }
        public Guid? ParentId { get; set; }
        public Guid? ChildId { get; set; }
    }
}
