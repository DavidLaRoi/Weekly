using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Weekly.API.DI
{
    public partial class TypeSearcher
    {
        private readonly IEnumerable<ITypeFilter> typeFilters1;

        public TypeSearcher()
        {
            List<ITypeFilter> filters = new List<ITypeFilter>();
            AddFilters(filters);
            typeFilters1 = filters;
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            Assembly assy = typeof(TypeSearcher).Assembly;
            foreach (Type type in assy.GetTypes())
            {
                ITypeFilter _ = typeFilters1.FirstOrDefault((x) => x.RegisterService(type, serviceCollection));
            }
        }

        partial void AddFilters(ICollection<ITypeFilter> typeFilters);

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class DontAutoMap : Attribute
    {

    }

    public interface ITypeFilter
    {
        bool RegisterService(Type type, IServiceCollection serviceCollection);
    }
}
