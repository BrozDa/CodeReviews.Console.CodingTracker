using Dapper;
using System.Data.SQLite;

namespace CodingTracker
{
    internal class CodingSessionRepository : ICodingSessionRepository
    {
        private readonly string _connectionString;
        public string RepositoryPath { get; }

        public CodingSessionRepository(string connectionString, string repositoryPath)
        {
            _connectionString = connectionString;
            RepositoryPath = repositoryPath;
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

        public void Insert(CodingSession entity)
        {
            string sql = "INSERT INTO [CodingSessions] ([Start], [End], [Duration]) VALUES (@Start, @End, @Duration);";

            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute(sql, entity);
        }

        public void Delete(CodingSession entity)
        {
            string sql = "DELETE FROM [CodingSessions] WHERE Id=@Id;";
            using var connection = new SQLiteConnection(_connectionString);
            connection.Execute(sql, new { Id = entity.Id });
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

        public IEnumerable<CodingSession> GetDataWithinRange(DateTime startDate, DateTime endDate)
        {
            string sql = "SELECT * FROM [CodingSessions] WHERE [Start] >= @start AND [END]<@end;";

            using var connection = new SQLiteConnection(_connectionString);
            var sessions = connection.Query<CodingSession>(sql, new { start = startDate, end = endDate });

            return sessions;
        }
    }
}