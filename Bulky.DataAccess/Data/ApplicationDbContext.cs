using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using BulkyBook.Models.Models;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
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
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "Tech Solution",
                    StreetAddress = "123 Tech St",
                    City = "Tech City",
                    PostalCode = "12121",
                    State = "IL",
                    PhoneNumber = "6665658791"
                },
                new Company
                {
                    Id = 2,
                    Name = "Vivid Book",
                    StreetAddress = "999 Vid St",
                    City = "Vid City",
                    PostalCode = "665235",
                    State = "IL",
                    PhoneNumber = "77788561235"
                },
                new Company
                {
                    Id = 3,
                    Name = "Reader Club",
                    StreetAddress = "99 Main St",
                    City = "Lala Land",
                    PostalCode = "999999",
                    State = "NY",
                    PhoneNumber = "5621453378"
                }
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
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
