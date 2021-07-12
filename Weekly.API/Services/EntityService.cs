using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weekly.API.Services
{
    public abstract class EntityService<TEntity, TDto, TContext> 
        where TContext : DbContext 
        where TEntity : DB.Models.Entity, new()
        where TDto : Data.Dto, new()
    {
        protected abstract TContext GetContext();

        protected abstract TContext Context { get; }

        protected abstract IMapper<TDto, TEntity> GetMapper();

        protected abstract IMapper<TDto, TEntity> Mapper { get; }

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
            GetMapper().MapToDto(entity, dto);

            return dto;
        }

        public virtual async Task UpsertItem(TDto dto)
        {
            TContext context = GetContext();
            DbSet<TEntity> set = context.Set<TEntity>();

            var found = await set.FindAsync(dto.Id);
            var entity = found ?? new TEntity() { Id = Guid.NewGuid() };

            GetMapper().MapToEntity(dto, entity);
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

            GetMapper().MapToEntity(dto, existing);

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
            GetMapper().MapToEntity(dto, entity);
            set.Add(entity);

            await context.SaveChangesAsync();
        }
    }
}
