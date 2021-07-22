namespace Weekly.Mappers
{
    public interface IMappingMediator
    {
        void Map<TSource, TTarget>(TSource source, TTarget target);
        IMappingMediator Require<TSource, TTarget>();
    }
}