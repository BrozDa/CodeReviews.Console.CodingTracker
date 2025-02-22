namespace CodingTracker
{
    internal interface ICodingSessionRepository<T> 
    {
        void Add(T entity); 
        IEnumerable<T> GetAll();
        void Update(T entity);
        void Delete(int id); 


    }
}
