using Microsoft.EntityFrameworkCore;
using NotisissimusSimpleSearch.Models;
using System.Reflection.Emit;

namespace NotisissimusSimpleSearch.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasIndex(p => new { p.Name, p.Description })
                .HasMethod("GIN")
                .IsTsVectorExpressionIndex("english");
        }
    }
}
