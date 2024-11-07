

namespace pragueParkingV2.Core.Models
{
    public abstract class Vehicle
    {
        public string LicensePlate { get; set; } // Registreringsnummer för fordonet
        public DateTime ParkingTime { get; set; } // Tidpunkt då fordonet parkerades

        // Egenskap för att definiera antal gratis minuter (standardvärde 10)
        public virtual int FreeMinutes { get; } = 10;

        // Konstruktör som initierar registreringsnummer och sätter parkeringsstarttiden
        public Vehicle(string licensePlate)
        {
            LicensePlate = licensePlate;
            ParkingTime = DateTime.Now;
        }

        // Abstrakt metod för att beräkna parkeringsavgiften, måste implementeras av underklasser
        public abstract decimal CalculateParkingFee();
    }
}
