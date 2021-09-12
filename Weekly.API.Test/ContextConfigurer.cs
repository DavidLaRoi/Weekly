using Microsoft.EntityFrameworkCore;
using Weekly.DB.Models;

namespace Weekly.API.Test
{
    public class ContextConfigurer : IContextConfigurer
    {
        public void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Weekly");
        }
    }

}