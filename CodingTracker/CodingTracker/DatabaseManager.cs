using Dapper;
using System.Data.SQLite;

namespace CodingTracker
{
    /// <summary>
    /// Managing reading and writing to the Coding Tracker database
    /// </summary>
    internal class DatabaseManager
    {
        public string ConnectionString { get; init; }
        public string DatabaseName { get; init; }
        public DatabaseManager(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;    
            DatabaseName = databaseName;
        }
        private void CreateDatabase()
        {
            string sql = $"CREATE DABASE {DatabaseName};";

            using (var connection = new SQLiteConnection(ConnectionString)) 
            { 
                connection.Execute(sql);
            }
        }
        

    }
}
