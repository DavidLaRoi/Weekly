using Microsoft.Extensions.DependencyInjection;

namespace Weekly.API.Client
{
    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection collection)
        {
            collection.AddSingleton<IStaticsClient, StaticsClient>();
        }
    }
}
