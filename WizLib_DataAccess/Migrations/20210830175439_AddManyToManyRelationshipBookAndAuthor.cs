using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class AddManyToManyRelationshipBookAndAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Fluent_Authors_Fluent_AuthorAuthor_Id",
                table: "BookAuthors");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthors_Fluent_AuthorAuthor_Id",
                table: "BookAuthors");

            migrationBuilder.DropColumn(
                name: "Fluent_AuthorAuthor_Id",
                table: "BookAuthors");

            migrationBuilder.CreateTable(
                name: "Fluent_BookAuthors",
                columns: table => new
                {
                    Book_Id = table.Column<int>(type: "int", nullable: false),
                    Author_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluent_BookAuthors", x => new { x.Author_Id, x.Book_Id });
                    table.ForeignKey(
                        name: "FK_Fluent_BookAuthors_Fluent_Authors_Author_Id",
                        column: x => x.Author_Id,
                        principalTable: "Fluent_Authors",
                        principalColumn: "Author_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fluent_BookAuthors_Fluent_Books_Book_Id",
                        column: x => x.Book_Id,
                        principalTable: "Fluent_Books",
                        principalColumn: "Book_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_BookAuthors_Book_Id",
                table: "Fluent_BookAuthors",
                column: "Book_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fluent_BookAuthors");

            migrationBuilder.AddColumn<int>(
                name: "Fluent_AuthorAuthor_Id",
                table: "BookAuthors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_Fluent_AuthorAuthor_Id",
                table: "BookAuthors",
                column: "Fluent_AuthorAuthor_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Fluent_Authors_Fluent_AuthorAuthor_Id",
                table: "BookAuthors",
                column: "Fluent_AuthorAuthor_Id",
                principalTable: "Fluent_Authors",
                principalColumn: "Author_Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
