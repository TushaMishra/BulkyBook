using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        /*        public DbSet<className> TableName { get; set; }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*base.OnModelCreating(modelBuilder);*/

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Title = "Rock in the Ocean",
                    Author = "Ron parker",
                    Discription = "This is a book to know the world under ocean",
                    ISBN = "SJO111212222",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl = "",
                },
                new Product()
                {
                    Id = 2,
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Discription = "This is a book to know the world under ocean",
                    ISBN = "SJO111212222",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryId = 1,
                    ImageUrl = "",
                },
                new Product()
                {
                    Id = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Discription = "This is a book to know the world under ocean",
                    ISBN = "SJO111212222",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId = 2,
                    ImageUrl = "",
                },
                new Product()
                {
                    Id = 4,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Discription = "This is a book to know the world under ocean",
                    ISBN = "SJO111212222",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl = "",
                },
                new Product()
                {
                    Id = 5,
                    Title = "Forune of Time",
                    Author = "Billy Spark",
                    Discription = "This is a book to know the world under ocean",
                    ISBN = "SJO111212222",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
                    CategoryId = 2,
                    ImageUrl = "",
                },
                new Product()
                {
                    Id = 6,
                    Title = "Rock in the Ocean",
                    Author = "Ron parker",
                    Discription = "This is a book to know the world under ocean",
                    ISBN = "SJO111212222",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 3,
                    ImageUrl = "",
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
