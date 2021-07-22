using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weekly.API.Mappers
{
    public static class MapperExtensions
    {
        public static TDto CreateDto<TEntity, TDto>(this IMapper<TEntity, TDto> mapper, TEntity entity) where TDto: new()
        {
            var dto = new TDto();
            mapper.MapToDto(entity, dto);
            return dto;
        }

        public static TEntity CreateEntity<TEntity, TDto>(this IMapper<TEntity, TDto> mapper, TDto dto) where TEntity : new()
        {
            var entity = new TEntity();
            mapper.MapToEntity(dto, entity);
            return entity;
        }
    }
}
