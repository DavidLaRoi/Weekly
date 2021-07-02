using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Weekly.DB.Models;

namespace Weekly.Commands
{
    class Program
    {
        private static readonly IServiceProvider Provider;
        static Program()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddTransient<IConnectionStringProvider, ConnectionStringProvider>();
            serviceCollection.AddSingleton<IContextConfigurer, DefaultContextConfigurer>();
            serviceCollection.AddScoped<WeeklyContext>();

            Provider = serviceCollection.BuildServiceProvider();
        }

        private class ConnectionStringProvider : IConnectionStringProvider
        {
            public string GetConnectionString() => "Server=DESKTOP-V2N2JBV\\SQLEXPRESS;Database=Weekly;Trusted_Connection=True;";
        }

        static void Main(string[] args)
        {
            using var scope = Provider.CreateScope();
            var provider = scope.ServiceProvider;

            var context = provider.GetService<WeeklyContext>();

            var items = context.Tasks.Include((x) => x.TaskHasTaskParentTasks).ToList();

            var uber = items.Where((x) => x.Name.ToLower().Contains("uber")).FirstOrDefault();

            var child = uber.Children.FirstOrDefault();

            var result = context.VerifyChildTaskEntry(child, uber);
            result = context.VerifyChildTaskEntry(child, child);
            result = context.VerifyChildTaskEntry(uber, child);
        }
    }
}
