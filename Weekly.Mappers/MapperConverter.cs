namespace Weekly.Mappers
{
    /// <summary>
    /// A converter that uses a mapper.
    /// </summary>
    public class MapperConverter<TSource, TTarget> 
        : IConverter<TSource, TTarget> 
        where TTarget : new()
    {
        private IMapper<TSource, TTarget> mapper;

        public MapperConverter(IMapper<TSource, TTarget> mapper)
        {
            this.mapper = mapper;
        }

        public TTarget Convert(TSource source)
        {
            var target = new TTarget();
            mapper.Map(source, target);
            return target;
        }
    }
}
