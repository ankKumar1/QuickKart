using Microsoft.EntityFrameworkCore;

namespace PurchaseService.Models
{
    public class PurchaseDBContext : DbContext
    {
        public PurchaseDBContext()
        {

        }
        public PurchaseDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("PurchaseDBConnectionString");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Purchase>().HasData(
                new Purchase
                {
                    PurchaseId = 1,
                    EmailId = "Franken@gmail.com",
                    ProductId = "P101",
                    QuantityPurchased = 2,
                    TotalPrice = 9000
                },
                new Purchase
                {
                    PurchaseId = 2,
                    EmailId = "Franken@gmail.com",
                    ProductId = "P143",
                    QuantityPurchased = 1,
                    TotalPrice = 1114.52M
                },
                new Purchase
                {
                    PurchaseId = 3,
                    EmailId = "SamRock@gmail.com",
                    ProductId = "P120",
                    QuantityPurchased = 4,
                    TotalPrice = 2085
                },
                new Purchase
                {
                    PurchaseId = 4,
                    EmailId = "PaulGrey@gmail.com",
                    ProductId = "P148",
                    QuantityPurchased = 1,
                    TotalPrice = 2561.51M
                },
                new Purchase
                {
                    PurchaseId = 5,
                    EmailId = "PaulGrey@gmail.com",
                    ProductId = "P101",
                    QuantityPurchased = 10,
                    TotalPrice = 1265
                }
            );

        }

    }
}
