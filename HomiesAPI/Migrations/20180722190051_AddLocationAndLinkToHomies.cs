using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HomiesAPI.Migrations
{
    public partial class AddLocationAndLinkToHomies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Homies_HomieId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_Homies_HomieId",
                table: "CheckOuts");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Homies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Homies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Homies",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NickName",
                table: "Homies",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HomieId",
                table: "CheckOuts",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "CheckOuts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "HomieId",
                table: "CheckIns",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "CheckIns",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Street = table.Column<string>(nullable: true),
                    HouseNumber = table.Column<string>(nullable: true),
                    AreaCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Homies_HomieId",
                table: "CheckIns",
                column: "HomieId",
                principalTable: "Homies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_Homies_HomieId",
                table: "CheckOuts",
                column: "HomieId",
                principalTable: "Homies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Homies_HomieId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_Homies_HomieId",
                table: "CheckOuts");

            migrationBuilder.DropTable(
                name: "LocationHomies");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropColumn(
                name: "NickName",
                table: "Homies");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "CheckIns");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Homies",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Homies",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Homies",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "HomieId",
                table: "CheckOuts",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "HomieId",
                table: "CheckIns",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Homies_HomieId",
                table: "CheckIns",
                column: "HomieId",
                principalTable: "Homies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_Homies_HomieId",
                table: "CheckOuts",
                column: "HomieId",
                principalTable: "Homies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
