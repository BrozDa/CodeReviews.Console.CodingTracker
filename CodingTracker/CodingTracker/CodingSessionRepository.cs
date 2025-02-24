using Dapper;
using Spectre.Console;
using System.Data.SQLite;

namespace CodingTracker
{
    internal class CodingSessionRepository : ICodingSessionRepository<CodingSession>
    {

        private readonly string _connectionString;
        public readonly string RepositoryName;
        public CodingSessionRepository(string connectionString, string repositoryName)
        {
            _connectionString = connectionString;
            RepositoryName = repositoryName;    
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
            string sql = "INSERT INTO [CodingSessions] ([Start], [End]) VALUES (@Start, @End);";

            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute(sql, entity);
        }

        public void Delete(CodingSession entity)
        {
            string sql = "DELETE FROM [CodingSessions] WHERE Id=@Id;";
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute(sql, new {Id = entity.Id});
        }

        public IEnumerable<CodingSession> GetAll()
        {
            
            string sql = "SELECT [ID], [Start], [End] FROM [CodingSessions]";
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
        public void InsertBulk(IEnumerable<CodingSession> entities)
        {
            string sql = "INSERT INTO [CodingSessions] ([Start], [End], [Duration]) VALUES (@Start, @End, @Duration);";

            using var connection = new SQLiteConnection(_connectionString);

                foreach (var entity in entities)
                {
                    connection.Execute(sql, entity);
                }

            
        }
    }
}
