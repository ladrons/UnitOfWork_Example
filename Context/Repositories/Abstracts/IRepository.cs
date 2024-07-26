using System.Linq.Expressions;

namespace UnitOfWork_Example.Context.Repositories.Abstracts
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);
        T GetByIdWhitInclude(int id, params Expression<Func<T, object>>[] includeProperties);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
