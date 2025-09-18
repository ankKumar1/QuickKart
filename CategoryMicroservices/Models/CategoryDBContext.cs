using Microsoft.EntityFrameworkCore;
namespace CategoryMicroservices.Models
{
    public class CategoryDBContext: DbContext
    {
        public CategoryDBContext() { }
        public CategoryDBContext(DbContextOptions dbContextOptions) { }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("CategoryDBConnectionString");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Motors"
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Arts"
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Furniture"
                }
            );
        }
    }
}
