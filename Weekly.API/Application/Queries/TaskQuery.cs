using AutoMapper;
using System;
using System.Linq;

namespace Weekly.API.Application.Queries
{
    public class TaskQuery : DtoQuery<Data.Dtos.Task>
    {
        public TaskQuery(Guid id) : base(id)
        {
        }
    }


    public class TaskQueryHandler : DtoQueryHandler<TaskQuery, Data.Dtos.Task, DB.Models.Task>
    {
        public TaskQueryHandler(IMapper mapper, IQueryable<DB.Models.Task> entities) : base(mapper, entities)
        {
        }
    }
}
