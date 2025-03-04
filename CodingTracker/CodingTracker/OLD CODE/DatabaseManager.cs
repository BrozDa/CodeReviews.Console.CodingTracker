﻿using Dapper;
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

            using (var connection = new SQLiteConnection(ConnectionString)) 
            {
                connection.Open();
            }
        }
        public void CreateTable()
        {
            string sql = $"CREATE TABLE '{tableName}' (" +
                $"ID INTEGER PRIMARY KEY,"+
                $"Start TEXT,"+
                $"End TEXT,"+
                $"Duration TEXT"+
                $");";

            using(var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute(sql);
            }

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
        public void InsertRecord(CodingRecord record)
        {
            string sql = $"INSERT INTO {tableName} (Start, End, Duration) " +
                $"VALUES (@start," +
                $"@end," +
                $"@duration" +
                $");"; ;

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Execute(sql,
                    new
                    {
                        start = record.Start,
                        end = record.End,
                        duration = record.Duration
                    }
                 );
            }
        }
        public void InsertBulk(List<CodingRecord> records)
        {

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                string sql = $"INSERT INTO {tableName} (Start, End, Duration) " +
                $"VALUES (@start," +
                $"@end," +
                $"@duration" +
                $");";

                foreach (var record in records) 
                {
                    connection.Execute(sql, 
                        new {
                            start = record.Start,
                            end = record.End,
                            duration = record.Duration
                        }
                    ); 
                }
                
            }
        }

        public void DeleteRecord(int recordID)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                string sql = $"DELETE FROM {tableName} " +
                    $"WHERE ID=@id;";

                connection.Execute(sql, new { id = recordID });
            }
        }

        

    }
}
