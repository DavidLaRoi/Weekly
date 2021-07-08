using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

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
