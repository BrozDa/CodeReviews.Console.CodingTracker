namespace CodingTracker
{
    internal interface ICodingSessionRepository<CodingSession> 
    {
        void Insert(CodingSession entity); 
        IEnumerable<CodingSession> GetAll();
        void Update(CodingSession entity);
        void Delete(CodingSession entity);
        void CreateRepository();
        void InsertBulk(IEnumerable<CodingSession> entities);
        IEnumerable<CodingSession> GetDataWithinRange(DateTime startDate, DateTime endDate);

    }
}
