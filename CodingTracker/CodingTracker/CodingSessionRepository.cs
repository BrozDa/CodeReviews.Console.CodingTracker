using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    internal class CodingSessionRepository : ICodingSessionRepository<CodingSession>
    {

        private readonly string _connectionString;

        public CodingSessionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateRepository()
        {
            string sql = "CREATE TABLE [CodingSessions] (" +
                "Id INTEGER PRIMARY KEY," +
                "Start TEXT," +
                "End TEXT," +
                "Duration TEXT);";

            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute(sql);
        }

        public void Add(CodingSession entity)
        {
            string sql = "INSERT INTO [CodingSessions] ([Start], [End], [Duration]) VALUES (@Start, @End, @Duration);";

            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute(sql, entity);
        }

        public void Delete(CodingSession entity)
        {
            string sql = "DELETE FROM [CodingSessions] WHERE Id=@Id;";
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute(sql, entity);
        }

        public IEnumerable<CodingSession> GetAll()
        {
            
            string sql = "SELECT * FROM [CodingSessions]";
            using var connection = new SQLiteConnection(_connectionString);
            var sessions = connection.Query<CodingSession>(sql);
            return sessions;
        }

        public void Update(CodingSession entity)
        {
            string sql = "UPDATE [CodingSessions] " +
                "SET Start=@Start, End=@End, Duration=@Duration " +
                "WHERE Id=@Id;";

            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute(sql, entity);
        }
    }
}
