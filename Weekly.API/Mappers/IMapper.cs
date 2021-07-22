namespace Weekly.API.Mappers
{
    public interface IMapper<TEntity, TDto>
    {
        void MapToEntity(TDto dto, TEntity entity);

        void MapToDto(TEntity entity, TDto dto);
    }
}
