using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class BookingAddedToHotelsModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
