﻿using System.Configuration;
using System.Collections.Specialized;

namespace CodingTracker
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            string? connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
            string? databaseName = ConfigurationManager.AppSettings.Get("DatabaseName");
            string dateTimeFormat = "dd-MM-yyyy HH:mm";

            InputManager inputManager = new InputManager(dateTimeFormat);
            OutputManager outputManager = new OutputManager(dateTimeFormat);
            DatabaseManager databaseManager = new DatabaseManager(connectionString, databaseName);

            CodingTracker tracker = new CodingTracker(databaseManager, inputManager, outputManager);
            tracker.Start();


            string s = "a";
            s = s.ToLower().Trim().ToLower().ToUpper().Replace("a", "b");
            Console.WriteLine(s);

        }
    }
}
