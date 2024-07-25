using UnitOfWork_Example.Context.Repositories.Abstracts;
using UnitOfWork_Example.Models;

namespace UnitOfWork_Example.Context.WorkUnits.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<Author> Authors { get; }

        int Complete();
    }
}
