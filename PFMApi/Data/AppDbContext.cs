using PFMApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using PFMApi.Data.Entitties;

namespace PFMApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Db Set 
        public DbSet<Transactions> Transactions { get; set; }

        public DbSet<Categories> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
