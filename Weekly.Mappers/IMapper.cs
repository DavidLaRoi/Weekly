namespace Weekly.Mappers
{

    public interface IMapper<TSource, TTarget>
    {

        void Map(TSource source, TTarget target);
    }


}
