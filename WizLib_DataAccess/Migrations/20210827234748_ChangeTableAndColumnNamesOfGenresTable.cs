using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class ChangeTableAndColumnNamesOfGenresTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Db_Genre");

            migrationBuilder.RenameColumn(
                name: "GenreName",
                table: "Db_Genre",
                newName: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Db_Genre",
                table: "Db_Genre",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Db_Genre",
                table: "Db_Genre");

            migrationBuilder.RenameTable(
                name: "Db_Genre",
                newName: "Genres");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Genres",
                newName: "GenreName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "GenreId");
        }
    }
}
