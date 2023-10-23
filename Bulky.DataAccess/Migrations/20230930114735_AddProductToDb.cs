using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BulkyBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProductToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Discription", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Ron parker", "This is a book to know the world under ocean", "SJO111212222", 30.0, 27.0, 20.0, 25.0, "Rock in the Ocean" },
                    { 2, "Abby Muscles", "This is a book to know the world under ocean", "SJO111212222", 70.0, 65.0, 55.0, 60.0, "Cotton Candy" },
                    { 3, "Julian Button", "This is a book to know the world under ocean", "SJO111212222", 55.0, 50.0, 35.0, 40.0, "Vanish in the Sunset" },
                    { 4, "Nancy Hoover", "This is a book to know the world under ocean", "SJO111212222", 40.0, 30.0, 20.0, 25.0, "Dark Skies" },
                    { 5, "Billy Spark", "This is a book to know the world under ocean", "SJO111212222", 99.0, 90.0, 80.0, 85.0, "Forune of Time" },
                    { 6, "Ron parker", "This is a book to know the world under ocean", "SJO111212222", 30.0, 27.0, 20.0, 25.0, "Rock in the Ocean" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
