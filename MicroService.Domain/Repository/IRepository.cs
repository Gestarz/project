namespace MicroService.Domain.Repository
{
    public interface IRepository<T>
    {

        T GetByGuid(Guid guid);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
