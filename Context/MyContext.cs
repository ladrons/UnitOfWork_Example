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

        public DbSet<Product> Products { get; set; }
    }
}