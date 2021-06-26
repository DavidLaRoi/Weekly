namespace Weekly.DB.Models
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString();
    }
}