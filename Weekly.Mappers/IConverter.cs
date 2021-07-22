namespace Weekly.Mappers
{
    internal interface IConverter<TSource, TTarget>
    {
        TTarget Convert(TSource source);
    }
}
