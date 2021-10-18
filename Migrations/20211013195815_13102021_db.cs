using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.Migrations
{
    public partial class _13102021_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Kullanicilar");
        }
    }
}
