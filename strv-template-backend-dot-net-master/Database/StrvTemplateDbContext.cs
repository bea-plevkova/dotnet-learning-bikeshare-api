using Microsoft.EntityFrameworkCore;
using Database.Entities;
using Database.Entities.Configurations;

namespace Database
{
    public class StrvTemplateDbContext : DbContext
    {
        public StrvTemplateDbContext()
        {
        }

        public StrvTemplateDbContext(DbContextOptions<StrvTemplateDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProducCategoryConfiguration());
        }

    }
}
