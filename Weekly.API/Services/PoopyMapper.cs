using System;

namespace Weekly.API.Services
{
    [Obsolete("Create specific mappers instead")]
    public class PoopyMapper<TDto, TEntity> : IMapper<TDto, TEntity>
    {
        public MapToDtoDelegate MapToDto { get; set; }

        public MapToEntityDelegate MapToEntity { get; set; }

        void IMapper<TDto, TEntity>.MapToDto(TEntity entity, TDto dto) => MapToDto(entity, dto);
        void IMapper<TDto, TEntity>.MapToEntity(TDto dto, TEntity entity) => MapToEntity(dto, entity);

        public delegate void MapToDtoDelegate(TEntity entity, TDto dto);

        public delegate void MapToEntityDelegate(TDto dto, TEntity entity);
    }
}
