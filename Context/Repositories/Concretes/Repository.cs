using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        public T GetById(int id) => _dbSet.Find(id);
        public T GetByIdWhitInclude(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault(e => EF.Property<int>(e, "Id") == id);
        }

        public void Add(T entity) => _dbSet.Add(entity);
        public void Delete(T entity) => _dbSet.Remove(entity);
        public void Update(T entity) => _dbSet.Update(entity);


    }
}