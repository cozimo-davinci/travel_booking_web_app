using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace COMP2139_Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class CarsFlightsFirstUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Booking_BookingsBookingId",
                table: "Flights");

            migrationBuilder.DropIndex(
                name: "IX_Flights_BookingsBookingId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Flights");

            migrationBuilder.RenameColumn(
                name: "PassengersBooked",
                table: "Flights",
                newName: "PassengersBookedTotal");

            migrationBuilder.RenameColumn(
                name: "MaxPassengers",
                table: "Flights",
                newName: "MaxPassengersTotal");

            migrationBuilder.RenameColumn(
                name: "BookingsBookingId",
                table: "Flights",
                newName: "PassengersBookedEconomy");

            migrationBuilder.AddColumn<float>(
                name: "BusinessPrice",
                table: "Flights",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "EconomyPrice",
                table: "Flights",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "FlightCode",
                table: "Flights",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxPassengersBusiness",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxPassengersEconomy",
                table: "Flights",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PassengersBookedBusiness",
                table: "Flights",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalFlightTime",
                table: "Flights",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessPrice",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "EconomyPrice",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "FlightCode",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "MaxPassengersBusiness",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "MaxPassengersEconomy",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "PassengersBookedBusiness",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "TotalFlightTime",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "PassengersBookedTotal",
                table: "Flights",
                newName: "PassengersBooked");

            migrationBuilder.RenameColumn(
                name: "PassengersBookedEconomy",
                table: "Flights",
                newName: "BookingsBookingId");

            migrationBuilder.RenameColumn(
                name: "MaxPassengersTotal",
                table: "Flights",
                newName: "MaxPassengers");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Flights",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_BookingsBookingId",
                table: "Flights",
                column: "BookingsBookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Booking_BookingsBookingId",
                table: "Flights",
                column: "BookingsBookingId",
                principalTable: "Booking",
                principalColumn: "BookingId");
        }
    }
}
