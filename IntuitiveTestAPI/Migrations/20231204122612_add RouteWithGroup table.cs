using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntuitiveTestAPI.Migrations
{
    /// <inheritdoc />
    public partial class addRouteWithGrouptable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RouteWithGroup",
                columns: table => new
                {
                    RouteWithGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartureAirportGroupID = table.Column<int>(type: "int", nullable: false),
                    ArrivalAirportGroupID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteWithGroup", x => x.RouteWithGroupID);
                    table.ForeignKey(
                        name: "FK_RouteWithGroup_AirportGroups_ArrivalAirportGroupID",
                        column: x => x.ArrivalAirportGroupID,
                        principalTable: "AirportGroups",
                        principalColumn: "AirportGroupID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RouteWithGroup_AirportGroups_DepartureAirportGroupID",
                        column: x => x.DepartureAirportGroupID,
                        principalTable: "AirportGroups",
                        principalColumn: "AirportGroupID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteWithGroup_ArrivalAirportGroupID",
                table: "RouteWithGroup",
                column: "ArrivalAirportGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_RouteWithGroup_DepartureAirportGroupID",
                table: "RouteWithGroup",
                column: "DepartureAirportGroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteWithGroup");
        }
    }
}
