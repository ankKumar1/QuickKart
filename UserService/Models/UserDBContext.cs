using Microsoft.EntityFrameworkCore;

namespace UserService.Models
{
    public class UserDBContext : DbContext
    {
        public UserDBContext()
        {

        }

        public UserDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("UserDBConnectionString");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    EmailId = "Franken@gmail.com",
                    UserPassword = "Franken@785",
                    RoleName = "Admin",
                    Gender = 'M',
                    DateOfBirth = DateTime.Parse("1978-09-10"),
                    Address = "Texas, USA"
                },
                new User
                {
                    EmailId = "PaulGrey@gmail.com",
                    UserPassword = "Paul@123",
                    RoleName = "User",
                    Gender = 'M',
                    DateOfBirth = DateTime.Parse("1993-07-07"),
                    Address = "Denver, USA"
                },
                new User
                {
                    EmailId = "SamRocks@gmail.com",
                    UserPassword = "Sam@564",
                    RoleName = "User",
                    Gender = 'M',
                    DateOfBirth = DateTime.Parse("1986-03-03"),
                    Address = "Denver, USA"
                }
            );
        }
    }
}
