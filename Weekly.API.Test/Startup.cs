using Microsoft.Extensions.DependencyInjection;

namespace Weekly.API_Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<Weekly.DB.Models.WeeklyContext>();
            services.AddTransient<Weekly.DB.Models.IContextConfigurer, ContextConfigurer>();
        }
    }
}
