using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Weekly.DB;
using TaskDto = Weekly.Data.Task;

namespace Weekly.API.Services
{
    public class TaskService : EntityService<DB.Models.Task,Data.Task,DB.Models.WeeklyContext>
    {
        private readonly DB.Models.WeeklyContext weeklyContext;

        protected override DB.Models.WeeklyContext Context => weeklyContext;

        protected override IMapper<TaskDto, DB.Models.Task> Mapper => new PoopyMapper<TaskDto, DB.Models.Task> { MapToDto = MapToDto, MapToEntity = MapToEntity };

        public TaskService(DB.Models.WeeklyContext weeklyContext)
        {
            this.weeklyContext = weeklyContext;
        }

        protected override DB.Models.WeeklyContext GetContext() => weeklyContext;

        static void MapToDto(DB.Models.Task entity, TaskDto dto)
        {
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            //TODO : refactor into seperate mapper class.
            dto.Group = new Data.Group
            {
                Description = dto.Group.Description,
                Name = dto.Group.Name,
                Id = dto.Id
            };
        }

        static void MapToEntity(TaskDto dto, DB.Models.Task entity)
        {
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Group = null;
            entity.GroupId = dto.Group?.Id;
        }

        protected override IEnumerable<string> Validate(TaskDto dto)
        {
            if (dto.Name is null) yield return "name cannot be null!";
        }

        protected override IQueryable<DB.Models.Task> Includes(IQueryable<DB.Models.Task> entities)
        {
            return entities.Include((x) => x.Group);
        }

        protected override IMapper<TaskDto, DB.Models.Task> GetMapper()
        {
            return new PoopyMapper<TaskDto, DB.Models.Task>
            {
                MapToDto = MapToDto,
                MapToEntity = MapToEntity
            };
        }
    }
}
