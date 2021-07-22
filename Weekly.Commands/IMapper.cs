using Microsoft.Extensions.DependencyInjection;
using System;

namespace Weekly.Commands
{

    public interface IMapper<TSource, TTarget>
    {

        void Map(TSource source, TTarget target);
    }

    public class MappingMediator : IMappingMediator
    {
        private readonly IServiceProvider serviceProvider;

        public void Map<TSource, TTarget>(TSource source, TTarget target)
        {
            var mapper = serviceProvider.GetRequiredService<IMapper<TSource, TTarget>>();
            mapper.Map(source, target);
        }

        public IMappingMediator Require<TSource, TTarget>()
        {
            serviceProvider.GetRequiredService<IMapper<TSource, TTarget>>();
            return this;
        }

    }
}
