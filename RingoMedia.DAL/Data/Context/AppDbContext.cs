using Microsoft.EntityFrameworkCore;
using RingoMedia.DAL.Data.Models;

namespace RingoMedia.DAL.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }


        public AppDbContext()
        {

        }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
