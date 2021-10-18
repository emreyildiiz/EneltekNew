using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.Migrations
{
    public partial class createe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Kullanicilar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "YetkiId",
                table: "Kullanicilar",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Kullanicilar");

            migrationBuilder.DropColumn(
                name: "YetkiId",
                table: "Kullanicilar");
        }
    }
}
