using UnitOfWork_Example.Context.Repositories.Abstracts;
using UnitOfWork_Example.Context.Repositories.Concretes;
using UnitOfWork_Example.Context.WorkUnits.Interfaces;
using UnitOfWork_Example.Models;


namespace UnitOfWork_Example.Context.WorkUnits
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext _context;

        public IRepository<Category> Categories { get; private set; }
        public IRepository<Product> Products { get; private set; }

        public UnitOfWork(MyContext context)
        {
            _context = context;

            Categories = new Repository<Category>(_context);
            Products = new Repository<Product>(_context);
        }


        public int Complete() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}
