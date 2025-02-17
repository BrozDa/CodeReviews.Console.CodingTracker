using System.Configuration;
using System.Collections.Specialized;

namespace CodingTracker
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            string? attr = ConfigurationManager.AppSettings.Get("DatabasePath");
            Console.WriteLine(attr);

            OutputManager output = new OutputManager();
            output.PrintMenu();
            Console.ReadLine();
        }
    }
}
