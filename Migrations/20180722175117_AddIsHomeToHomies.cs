using Microsoft.EntityFrameworkCore.Migrations;

namespace HomiesAPI.Migrations
{
    public partial class AddIsHomeToHomies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHome",
                table: "Homies",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHome",
                table: "Homies");
        }
    }
}
