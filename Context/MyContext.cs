using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using UnitOfWork_Example.Models;

namespace UnitOfWork_Example.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);
        }

        public DbSet<Book> Books{ get; set; }
        public DbSet<Author> Authors{ get; set; }
    }
}