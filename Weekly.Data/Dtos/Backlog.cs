using System.Collections.Generic;

namespace Weekly.Data.Dtos
{
    public class Backlog : UserDto
    {
        public Group Group { get; set; }

        public List<BacklogItem> Items { get; set; }
    }

}
