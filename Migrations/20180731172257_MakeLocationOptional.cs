using Microsoft.EntityFrameworkCore.Migrations;

namespace HomiesAPI.Migrations
{
    public partial class MakeLocationOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homies_Locations_LocationId",
                table: "Homies");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Homies",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Homies_Locations_LocationId",
                table: "Homies",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Homies_Locations_LocationId",
                table: "Homies");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Homies",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Homies_Locations_LocationId",
                table: "Homies",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
