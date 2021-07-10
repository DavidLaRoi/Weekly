using Microsoft.EntityFrameworkCore;

namespace Weekly.DB.Models
{
    public class DefaultContextConfigurer : IContextConfigurer
    {
        private readonly IConnectionStringProvider connectionStringProvider;

        public DefaultContextConfigurer(IConnectionStringProvider connectionStringProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
        }

        public void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionStringProvider.GetConnectionString());

            }
        }
    }

    public class DefaultConnectionStringProvider : IConnectionStringProvider
    {
        public string GetConnectionString()
        {
            return "Server=DESKTOP-V2N2JBV\\SQLEXPRESS;Database=Weekly;Trusted_Connection=True;";
        }
    }
}