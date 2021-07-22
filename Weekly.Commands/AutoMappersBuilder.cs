using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Weekly.Commands
{

    public class AutoMappersBuilder
    {
        public void Build()
        {

        }

        private readonly List<ObjectMapperBuilder> builders = new();

        internal static readonly Type IMapperType = typeof(IMapper<,>);
        internal static Type GenericMapperType(Type source, Type target) => IMapperType.MakeGenericType(source, target);

        public ObjectMapperBuilder<TSource, TTarget> AddMapper<TSource, TTarget>()
        {
            ObjectMapperBuilder<TSource, TTarget> builder = new ObjectMapperBuilder<TSource, TTarget>();
            builders.Add(builder);
            return builder;
        }

        public void BuildAndRegister(IServiceCollection serviceCollection)
        {

        }
    }

}
