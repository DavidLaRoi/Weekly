using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weekly.DB;
using TaskDto = Weekly.Data.Task;

namespace Weekly.API.Services
{
    public class TaskService : EntityService<DB.Models.Task,Data.Task,DB.Models.WeeklyContext>
    {
        private readonly DB.Models.WeeklyContext weeklyContext;

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

    public class PoopyMapper<TDto, TEntity> : IMapper<TDto, TEntity>
    {
        public MapToDtoDelegate MapToDto { get; set; }

        public MapToEntityDelegate MapToEntity { get; set; }

        void IMapper<TDto, TEntity>.MapToDto(TEntity entity, TDto dto) => MapToDto(entity, dto);
        void IMapper<TDto, TEntity>.MapToEntity(TDto dto, TEntity entity) => MapToEntity(dto, entity);

        public delegate void MapToDtoDelegate(TEntity entity, TDto dto);

        public delegate void MapToEntityDelegate(TDto dto, TEntity entity);
    }

    public interface IMapper<TDto,TEntity>
    {
        void MapToEntity(TDto dto, TEntity entity);

        void MapToDto(TEntity entity, TDto dto);
    }


    public abstract class EntityService<TEntity, TDto, TContext> 
        where TContext : DbContext 
        where TEntity : DB.Models.Entity, new()
        where TDto : Data.Dto, new()
    {
        protected abstract TContext GetContext();

        protected abstract IMapper<TDto, TEntity> GetMapper();

        protected virtual IQueryable<TEntity> Includes(IQueryable<TEntity> entities) => entities;

        protected virtual IEnumerable<string> Validate(TDto dto)
        {
            yield break;
        }

        public virtual async Task<TDto> GetItem(Guid id)
        {
            TContext context = GetContext();
            DbSet<TEntity> set = context.Set<TEntity>();

            var entity = await set.FindAsync(id);
            var dto = new TDto();
            MapToDto(entity, dto);

            return dto;
        }

        public virtual async Task UpsertItem(TDto dto)
        {
            TContext context = GetContext();
            DbSet<TEntity> set = context.Set<TEntity>();

            var found = await set.FindAsync(dto.Id);
            var entity = found ?? new TEntity() { Id = Guid.NewGuid() };

            MapToEntity(dto, entity);
            if (found is null) set.Add(found);

            await context.SaveChangesAsync();
        }

        public virtual async Task UpdateItem(TDto dto)
        {
            //TODO specify exceptions
            if (!dto.Id.HasValue) throw new Exception();

            //TODO specify exceptions, use error messages
            if (Validate(dto).Any()) throw new Exception();

            TContext context = GetContext();
            DbSet<TEntity> set = context.Set<TEntity>();

            var existing = await set.FindAsync(dto.Id);

            //TODO specify exceptions
            if (existing is null) throw new Exception();

            MapToEntity(dto, existing);

            await context.SaveChangesAsync();

        }

        public virtual async Task AddItem(TDto dto)
        {
            //TODO specify exception
            if (dto.Id.HasValue) throw new Exception();

            //TODO specify exceptions, use errors
            if (Validate(dto).Any()) throw new Exception();

            TContext context = GetContext();
            DbSet<TEntity> set = context.Set<TEntity>();

            var entity = new TEntity();
            MapToEntity(dto, entity);
            set.Add(entity);

            await context.SaveChangesAsync();
        }
    }
}
