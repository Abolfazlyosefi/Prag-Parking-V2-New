namespace pragueParkingV2.Core.Models
{
    // Enum för olika storlekar på parkeringsplatser
    public enum ParkingSpotSize
    {
        Small,
        Medium,
        Large
    }

    public class ParkingSpot
    {
        public int SpotId { get; }
        public Vehicle? ParkedVehicle { get; set; }
        public bool IsOccupied => ParkedVehicle != null; // Egenskap för att kolla om platsen är upptagen
        public ParkingSpotSize Size { get; set; } // Egenskap för storlek på parkeringsplatsen

        // Konstruktör för ParkingSpot som initierar ID och storlek
        public ParkingSpot(int spotId, ParkingSpotSize size)
        {
            SpotId = spotId;
            Size = size;
        }

        // Metod för att parkera ett fordon
        public void Park(Vehicle vehicle)
        {
            ParkedVehicle = vehicle;
        }

        // Metod för att ta bort ett parkerat fordon
        public void RemoveVehicle()
        {
            ParkedVehicle = null;
        }

    
    }
}
