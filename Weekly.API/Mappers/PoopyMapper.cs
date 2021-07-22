using System;
using Weekly.API.DI;

namespace Weekly.API.Mappers
{
    [Obsolete("Create specific mappers instead")]
    [DontAutoMap]
    public class PoopyMapper<TEntity, TDto> : IMapper<TEntity, TDto>
    {
        public MapToDtoDelegate MapToDto { get; set; }

        public MapToEntityDelegate MapToEntity { get; set; }

        void IMapper<TEntity, TDto>.MapToDto(TEntity entity, TDto dto) => MapToDto(entity, dto);
        void IMapper<TEntity, TDto>.MapToEntity(TDto dto, TEntity entity) => MapToEntity(dto, entity);

        public delegate void MapToDtoDelegate(TEntity entity, TDto dto);

        public delegate void MapToEntityDelegate(TDto dto, TEntity entity);
    }
}
