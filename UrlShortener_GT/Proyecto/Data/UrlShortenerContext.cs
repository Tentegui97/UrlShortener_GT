using Microsoft.EntityFrameworkCore;
using UrlShortener_GT.Proyecto.Entities;

namespace UrlShortener_GT.Proyecto.Data
{
    public class UrlShortenerContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        public UrlShortenerContext(DbContextOptions<UrlShortenerContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User Admin = new User()
            {
                Id = 1,
                Username = "Admin",
                Password = "123456789",
                Email = "admin@gmail.com",
            };
            User luis = new User()
            {
                Id = 2,
                Username = "Gustavo Tentegui",
                Password = "987654321",
                Email = "gtentegui@gmail.com",
            };
            Category social = new Category()
            {
                Id = 1,
                CategoryName = "Informatica"
            };
            Category deportes = new Category()
            {
                Id = 2,
                CategoryName = "Civiles"
            };
            Category multimedia = new Category()
            {
                Id = 3,
                CategoryName = "Sociales"
            };
            modelBuilder.Entity<User>().HasData(
                Admin, luis);
            modelBuilder.Entity<Category>().HasData(
                social, deportes, multimedia);

            modelBuilder.Entity<Url>()
            .HasOne(url => url.User)
            .WithMany()
            .HasForeignKey(url => url.UserId);

            modelBuilder.Entity<Url>()
            .HasOne(u => u.Category)
            .WithMany()
            .HasForeignKey(u => u.CategoryId);


            base.OnModelCreating(modelBuilder);
        }
    }
}