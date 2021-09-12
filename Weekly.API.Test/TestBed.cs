using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weekly.DB.Models;
using Xunit;

namespace Weekly.API.Test
{
    public abstract class TestBed<TRequestHandler, TDbContext> : IDisposable
        where TDbContext : DbContext
        where TRequestHandler : class
    {
        #region ServiceProvider

        private IServiceProvider rootServiceProvider;

        protected IServiceProvider ServiceProvider => CurrentScope?.ServiceProvider ?? rootServiceProvider;

        private readonly List<IServiceScope> serviceScopes = new List<IServiceScope>();

        private IServiceScope CurrentScope => serviceScopes.Any() ? serviceScopes[^1] : null;

        #endregion

        public TestBed()
        {
            var services = new ServiceCollection();

            RegisterBaseServices(services);

            this.rootServiceProvider = services.BuildServiceProvider();
        }

        protected IDisposable CreateScope()
        {
            var newScope = ServiceProvider.CreateScope();
            serviceScopes.Add(newScope);
            return Weekly.Utils.Extensions.DoAtDispose(PopScope);
        }

        private void PopScope()
        {
            CurrentScope?.Dispose();
            serviceScopes.RemoveAt(serviceScopes.Count - 1);
        }

        private class ContextConfigurer : IContextConfigurer
        {
            public ContextConfigurer(Type type)
            {
                Type = type;
            }

            private Type Type { get; }

            public void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase(databaseName: Type.FullName);
            }
        }

        private void RegisterBaseServices(IServiceCollection services)
        {
            services.AddDbContext<TDbContext>();
            services.AddSingleton<IContextConfigurer, ContextConfigurer>(x => new ContextConfigurer(this.GetType()));
            services.AddAutoMapper(ConfigureMappers);
            services.AddScoped<TRequestHandler>();
            RegisterServices(services);
        }

        protected abstract void ConfigureMappers(IMapperConfigurationExpression mapperConfigurationExpression);

        protected virtual void RegisterServices(IServiceCollection services) { }

        protected IAsyncDisposable Arrange(out TDbContext dbContext)
        {
            var scoped = CreateScope();
            var context = ServiceProvider.GetRequiredService<TDbContext>();
            dbContext = context;

            return Weekly.Utils.Extensions.DoAtDisposeAsync(async () =>
            {
                await context.SaveChangesAsync();
                scoped.Dispose();
            });
        }

        protected IDisposable Act(out TRequestHandler handler)
        {
            var scoped = CreateScope();
            handler = ServiceProvider.GetRequiredService<TRequestHandler>();
            return scoped;
        }

        public void Dispose()
        {
            while (CurrentScope is not null) PopScope();
            ServiceProvider.GetRequiredService<TDbContext>().Database.EnsureDeleted();            
        }
    }
}
