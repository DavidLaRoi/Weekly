using Microsoft.EntityFrameworkCore;

namespace Weekly.DB.Models
{
    public interface IContextConfigurer
    {
        void OnConfiguring(DbContextOptionsBuilder optionsBuilder);
    }
}