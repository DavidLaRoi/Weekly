using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Weekly.API_Test.NewFolder
{
    public abstract class TestBed<TRequestHandler, TDbContext> where TDbContext : DbContext
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
            services.AddDbContext<TDbContext>(x => x.UseInMemoryDatabase(databaseName: this.GetType().FullName));
            RegisterServices(services);
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

        protected abstract void RegisterServices(IServiceCollection services);

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
    }
}
