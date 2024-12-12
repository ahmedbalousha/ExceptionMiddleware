using ExceptionMiddleware.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ExceptionMiddleware.API.Data
{
    public class ExceptionMiddlewareDbContext : DbContext
    {
        public ExceptionMiddlewareDbContext(DbContextOptions<ExceptionMiddlewareDbContext> options)
        : base(options) // Pass options to the base class constructor
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add your model configurations here

            base.OnModelCreating(modelBuilder); // Ensure the base class configurations are applied
        }


        public DbSet<Class> Classes { get; set; }

        public DbSet<Student> Students { get; set; }




    }
}
