using ConsoleAdapter;
using System;

namespace Weekly.Commands
{
    public class Program : ConsoleAdapter.Commands
    {
        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                Call<Program>(args);
            }
            else
            {
                Run<Program>();
            }
        }

        public void CreateOpenApi()
        {

        }

        [ConsoleVisible]
        public void RunNSwag(string swaggerFile, string targetDir)
        {

        }

        [ConsoleVisible]
        public async System.Threading.Tasks.Task Test(string message, int timeout)
        {
            Console.WriteLine(message);
            await System.Threading.Tasks.Task.Delay(timeout);
        }
    }
}
