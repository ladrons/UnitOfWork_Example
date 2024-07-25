using Microsoft.EntityFrameworkCore;
using UnitOfWork_Example.Context.Repositories.Abstracts;

namespace UnitOfWork_Example.Context.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MyContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(MyContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();

        }

        //---------------------------------------------------------------------------------------

        public IEnumerable<T> GetAll() => _dbSet.ToList();
        public T GetById(int id) => _dbSet.Find(id); //id değeri null gelirse hata verir. Bu noktada hata yönetimi yapılabilir.
        
        public void Add(T entity) => _dbSet.Add(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public void Update(T entity) => _dbSet.Update(entity);
    }
}