using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class BookingHotelsModelsUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Booking_BookingsBookingId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_BookingsBookingId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "BookingsBookingId",
                table: "Hotels");

            migrationBuilder.AddColumn<int>(
                name: "HotelsId",
                table: "Booking",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_HotelsId",
                table: "Booking",
                column: "HotelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Hotels_HotelsId",
                table: "Booking",
                column: "HotelsId",
                principalTable: "Hotels",
                principalColumn: "HotelsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Hotels_HotelsId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_HotelsId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "HotelsId",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "BookingsBookingId",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_BookingsBookingId",
                table: "Hotels",
                column: "BookingsBookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Booking_BookingsBookingId",
                table: "Hotels",
                column: "BookingsBookingId",
                principalTable: "Booking",
                principalColumn: "BookingId");
        }
    }
}
