using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HomiesAPI.Migrations
{
    public partial class CreateCheckOuts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WithGuest",
                table: "CheckIns",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CheckOuts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HomieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckOuts_Homies_HomieId",
                        column: x => x.HomieId,
                        principalTable: "Homies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_HomieId",
                table: "CheckOuts",
                column: "HomieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "WithGuest",
                table: "CheckIns");
        }
    }
}
