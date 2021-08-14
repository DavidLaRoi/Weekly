using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Weekly.API.DI
{
    public static class Extensions
    {
        public static void AddDbSet<TDbContext, TEntity>(this IServiceCollection services)
            where TDbContext : DbContext
            where TEntity : class
        {
            services.AddScoped(x => x.GetRequiredService<TDbContext>().Set<TEntity>());
            services.AddScoped(x => x.GetRequiredService<DbSet<TEntity>>().AsNoTracking());
        }


        public static void AddDbSets<TDbContext>(this IServiceCollection services)
             where TDbContext : DbContext
        {
            System.Reflection.MethodInfo method = typeof(Extensions).GetMethod(nameof(AddDbSet), new Type[] { typeof(IServiceCollection) });
            foreach (System.Reflection.PropertyInfo property in typeof(TDbContext).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
            {
                if (property.PropertyType is Type propType && propType.IsGenericType && propType.GetGenericTypeDefinition() == typeof(DbSet<>))
                {
                    method.MakeGenericMethod(typeof(TDbContext), propType.GetGenericArguments()[0]).Invoke(null, new object[] { services });
                }
            }
        }
    }

}
