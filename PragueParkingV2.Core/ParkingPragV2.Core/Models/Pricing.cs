namespace pragueParkingV2.Core.Models
{
    public class Pricing
    {
        public string VehicleType { get; set; } // Typ av fordon, t.ex. "Car" eller "Motorcycle"
        public decimal HourlyRate { get; set; } // Timpris
        public int FreeMinutes { get; set; } // Antal gratis minuter

        public Pricing(string vehicleType, decimal hourlyRate, int freeMinutes)
        {
            VehicleType = vehicleType;
            HourlyRate = hourlyRate;
            FreeMinutes = freeMinutes;
        }
    }
}
