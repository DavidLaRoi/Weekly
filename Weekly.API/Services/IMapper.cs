namespace Weekly.API.Services
{
    public interface IMapper<TDto, TEntity>
    {
        void MapToEntity(TDto dto, TEntity entity);

        void MapToDto(TEntity entity, TDto dto);
    }
}
