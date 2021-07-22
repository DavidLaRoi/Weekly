using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weekly.API.Mappers
{
    public class TaskMapper : IMapper<DB.Models.Task, Data.Task>
    {
        public void MapToDto(DB.Models.Task entity, Data.Task dto)
        {
            throw new NotImplementedException();
        }

        public void MapToEntity(Data.Task dto, DB.Models.Task entity)
        {
            throw new NotImplementedException();
        }
    }
}
