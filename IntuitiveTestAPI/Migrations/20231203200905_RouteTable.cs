using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntuitiveTestAPI.Migrations
{
    /// <inheritdoc />
    public partial class RouteTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    RouteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureAirportID = table.Column<int>(type: "int", nullable: false),
                    ArrivalAirportID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.RouteID);
                    table.ForeignKey(
                        name: "FK_Routes_Airports_ArrivalAirportID",
                        column: x => x.ArrivalAirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Routes_Airports_DepartureAirportID",
                        column: x => x.DepartureAirportID,
                        principalTable: "Airports",
                        principalColumn: "AirportID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Routes_ArrivalAirportID",
                table: "Routes",
                column: "ArrivalAirportID");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_DepartureAirportID",
                table: "Routes",
                column: "DepartureAirportID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Routes");
        }
    }
}
