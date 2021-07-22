using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Weekly.Mappers
{
    public static class DIExtensions
    {
        public static void AddMapper<TMapper,TSource,TTarget>(this IServiceCollection collection) 
            where TMapper : IMapper<TSource,TTarget>
        {
            collection.AddSingleton(typeof(IMapper<TSource, TTarget>), typeof(TMapper));
        }

        public static void AddMapperWithConverter<TMapper, TSource, TTarget>(this IServiceCollection collection) 
            where TMapper : IMapper<TSource, TTarget> 
            where TTarget : new()
        {
            collection.AddMapper<TMapper, TSource, TTarget>();
            collection.AddSingleton(typeof(IConverter<TSource, TTarget>), typeof(MapperConverter<TSource,TTarget>));
        }
    }
}
