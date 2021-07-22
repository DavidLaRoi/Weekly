using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Weekly.API.DI
{
    public partial class TypeSearcher
    {
        partial void AddFilters(ICollection<ITypeFilter> typeFilters)
        {
            typeFilters.Add(new MapperSearcher());
        }
    }

    public class MapperSearcher : ITypeFilter
    {
        public bool RegisterService(Type type, IServiceCollection serviceCollection)
        {
            var interfaces = type.GetInterfaces();
            if (interfaces.Any())
            {
                var inter = interfaces.FirstOrDefault((x) => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(Mappers.IMapper<,>));
                if(inter is Type)
                {
                    serviceCollection.AddSingleton(inter, type);
                    return true;
                }
            }
            return false;
        }
    }
}
