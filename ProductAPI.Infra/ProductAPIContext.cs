using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductAPI.Domain.Entities;
using ProductAPI.Infra.Mapping;

namespace ProductAPI.Infra
{
    public  class ProductAPIContext : DbContext
    {

        public ProductAPIContext(DbContextOptions<ProductAPIContext> options) : base(options) { }

        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMap());
        }
        public static void Configure(IServiceCollection services, string DefaultConnection, bool testing)
        {

            services.AddDbContext<ProductAPIContext>(options =>
            {
                if (testing)
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                }
                else
                {
                    options.UseNpgsql(DefaultConnection, b => b.MigrationsAssembly("ProductAPI.Infra"));
                }
            });
        }
    }
}