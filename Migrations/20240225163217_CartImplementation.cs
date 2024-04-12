using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class CartImplementation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CartID",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartID);
                });

            migrationBuilder.CreateTable(
                name: "FlightCarts",
                columns: table => new
                {
                    FlightID = table.Column<int>(type: "int", nullable: false),
                    CartID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightCarts", x => new { x.FlightID, x.CartID });
                    table.ForeignKey(
                        name: "FK_FlightCarts_CartItems_CartID",
                        column: x => x.CartID,
                        principalTable: "CartItems",
                        principalColumn: "CartID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightCarts_Flights_FlightID",
                        column: x => x.FlightID,
                        principalTable: "Flights",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CartID",
                table: "Hotels",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CartID",
                table: "Cars",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_FlightCarts_CartID",
                table: "FlightCarts",
                column: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CartItems_CartID",
                table: "Cars",
                column: "CartID",
                principalTable: "CartItems",
                principalColumn: "CartID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_CartItems_CartID",
                table: "Hotels",
                column: "CartID",
                principalTable: "CartItems",
                principalColumn: "CartID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CartItems_CartID",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_CartItems_CartID",
                table: "Hotels");

            migrationBuilder.DropTable(
                name: "FlightCarts");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_CartID",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CartID",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "CartID",
                table: "Cars");

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelsId = table.Column<int>(type: "int", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: true),
                    GuestUsedId = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: true),
                    RentalId = table.Column<int>(type: "int", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Booking_Hotels_HotelsId",
                        column: x => x.HotelsId,
                        principalTable: "Hotels",
                        principalColumn: "HotelsId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_HotelsId",
                table: "Booking",
                column: "HotelsId");
        }
    }
}
