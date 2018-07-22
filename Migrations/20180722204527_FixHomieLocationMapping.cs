using Microsoft.EntityFrameworkCore.Migrations;

namespace HomiesAPI.Migrations
{
    public partial class FixHomieLocationMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationHomies");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Homies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Homies_LocationId",
                table: "Homies",
                column: "LocationId");

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

            migrationBuilder.DropIndex(
                name: "IX_Homies_LocationId",
                table: "Homies");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Homies");

            migrationBuilder.CreateTable(
                name: "LocationHomies",
                columns: table => new
                {
                    HomieId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationHomies", x => new { x.HomieId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_LocationHomies_Homies_HomieId",
                        column: x => x.HomieId,
                        principalTable: "Homies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocationHomies_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationHomies_LocationId",
                table: "LocationHomies",
                column: "LocationId");
        }
    }
}
