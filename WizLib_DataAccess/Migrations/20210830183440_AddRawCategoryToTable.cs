using Microsoft.EntityFrameworkCore.Migrations;

namespace WizLib_DataAccess.Migrations
{
    public partial class AddRawCategoryToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO tbl_category (CategoryName) VALUES ('Category 1')");
            migrationBuilder.Sql("INSERT INTO tbl_category (CategoryName) VALUES ('Category 2')");
            migrationBuilder.Sql("INSERT INTO tbl_category (CategoryName) VALUES ('Category 3')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
