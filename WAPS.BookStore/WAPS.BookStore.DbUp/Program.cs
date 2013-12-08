using System;
using System.Linq;
using System.Reflection;
using DbUp;

namespace WAPS.BookStore.DbUp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = args.FirstOrDefault() ?? "Server=(local)\\SqlExpress; Database=WAPS_BookStore; Trusted_connection=true";

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            //Just Added to see information
            Console.WriteLine("\r\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
