using Microsoft.EntityFrameworkCore;

namespace ProductService.Models
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext()
        {

        }

        public ProductDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("ProductDBConnectionString");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = "P101",
                    ProductName = "Lamborghini Gallardo Spyder",
                    CategoryId = 1,
                    Price = 18000000,
                    QuantityAvailable = 10
                },
                new Product
                {
                    ProductId = "P102",
                    ProductName = "Harley Davidson Iron 883",
                    CategoryId = 1,
                    Price = 7000000,
                    QuantityAvailable = 10
                },
                new Product
                {
                    ProductId = "P103",
                    ProductName = "Oil painting on Canvas",
                    CategoryId = 2,
                    Price = 2056,
                    QuantityAvailable = 200
                },
                new Product
                {
                    ProductId = "P104",
                    ProductName = "Marble Elephants Statue",
                    CategoryId = 2,
                    Price = 9000,
                    QuantityAvailable = 100
                },
                new Product
                {
                    ProductId = "P105",
                    ProductName = "Dining Table",
                    CategoryId = 3,
                    Price = 15000,
                    QuantityAvailable = 50
                }
               );
        }
    }
}
