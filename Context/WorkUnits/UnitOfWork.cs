using UnitOfWork_Example.Context.Repositories.Abstracts;
using UnitOfWork_Example.Context.Repositories.Concretes;
using UnitOfWork_Example.Context.WorkUnits.Interfaces;
using UnitOfWork_Example.Models;

namespace UnitOfWork_Example.Context.WorkUnits
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _context;

        public IRepository<Book> Books { get; private set; }
        public IRepository<Author> Authors { get; private set; }

        public UnitOfWork(MyContext context)
        {
            _context = context;

            Books = new Repository<Book>(_context);
            Authors = new Repository<Author>(_context);
        }


        public int Complete() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}
