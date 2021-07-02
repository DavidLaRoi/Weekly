using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Weekly.DB.Models;

namespace Weekly.DB.Test
{
    [TestClass]
    public class UnitTest1
    {
        private IServiceProvider provider;
        public UnitTest1()
        {
            var serviceCollection = new ServiceCollection();

            //serviceCollection.AddSingleton<IConnectionStringProvider, ConnectionStringProvider>();
            serviceCollection.AddSingleton<IContextConfigurer, ContextConfigurer>();
            serviceCollection.AddScoped<WeeklyContext>();
            provider = serviceCollection.BuildServiceProvider();
        }

        [TestMethod]
        public void TestMethod1()
        {
            {
                using var scope = provider.CreateScope();
                using var context = scope.ServiceProvider.GetService<WeeklyContext>();
                context.Tasks.ToList();
                context.Tasks.Add(new Task() { Name = "boobs" });
                context.SaveChanges();
            }
            {
                using var scope = provider.CreateScope();
                using var context = scope.ServiceProvider.GetService<WeeklyContext>();
                var t = context.Tasks.Where((x) => x.Name == "boobs").FirstOrDefault();

            }
            {
                using var scope = provider.CreateScope();
            }
        }

        private class ContextConfigurer : IContextConfigurer
        {
            public void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseInMemoryDatabase("Context");
            }
        }
    }
}
