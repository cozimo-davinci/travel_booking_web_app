namespace COMP2139_Assignment1.Models
{
    public class Search
    {
        public List<Flights> Flights { get; set; } = new List<Flights>();

        public List<Hotels> Hotels { get; set; } = new List<Hotels>();

        public List<Cars> CarRentals { get; set; } = new List<Cars>();
    }
}
