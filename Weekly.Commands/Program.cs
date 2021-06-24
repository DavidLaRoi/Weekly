using System;

namespace Weekly.Commands
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var k = new Data.ActivityGroup();
            k.Name = "yeah";
            k.Description = "ok";
            Console.WriteLine(k.ToString());
        }
    }
}
