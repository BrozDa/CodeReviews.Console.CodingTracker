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
        private string tableName = "Sessions";
        public DatabaseManager(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;    
            DatabaseName = databaseName;
        }
        public bool DoesDatabaseExists() => File.Exists(DatabaseName);
        public void CreateDatabase()
        {
            string sql = $"CREATE DABASE {DatabaseName};";

            using (var connection = new SQLiteConnection(ConnectionString)) 
            { 
                connection.Execute(sql);
            }
        }
        public void CreateTable()
        {
            string sql = $"CREATE TABLE {tableName} (" +
                $"ID INTEGER,"+
                $"Start TEXT,"+
                $"End TEXT,"+
                $"Duration TEXT,"+
                $");";

        }
        public List<CodingRecord> GetRecords()
        {
            List<CodingRecord> sessions = new List<CodingRecord>();

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                string sql = $"SELECT * FROM {tableName};";
                sessions = connection.Query<CodingRecord>(sql).ToList();
            }
            return sessions;
        }
        

    }
}
