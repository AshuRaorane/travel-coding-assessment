using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntuitiveTestAPI.Migrations
{
    /// <inheritdoc />
    public partial class airportgrptableadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirportGroups",
                columns: table => new
                {
                    AirportGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportGroups", x => x.AirportGroupID);
                });

            migrationBuilder.CreateTable(
                name: "AirportInAirportGroups",
                columns: table => new
                {
                    AirportGroupID = table.Column<int>(type: "int", nullable: false),
                    AirportID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirportInAirportGroups", x => new { x.AirportID, x.AirportGroupID });
                    table.ForeignKey(
                        name: "FK_AirportInAirportGroups_AirportGroups_AirportGroupID",
                        column: x => x.AirportGroupID,
                        principalTable: "AirportGroups",
                        principalColumn: "AirportGroupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AirportInAirportGroups_Airports_AirportID",
                        column: x => x.AirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirportInAirportGroups_AirportGroupID",
                table: "AirportInAirportGroups",
                column: "AirportGroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirportInAirportGroups");

            migrationBuilder.DropTable(
                name: "AirportGroups");
        }
    }
}
