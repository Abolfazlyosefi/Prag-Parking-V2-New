namespace pragueParkingV2.Core.Models
{
    public class ParkedVehicle
    {
        public string LicensePlate { get; set; }
        public DateTime ParkingTime { get; set; }
        public string VehicleType { get; set; } 

        public ParkedVehicle() { }

        public ParkedVehicle(string licensePlate, DateTime parkingTime, string vehicleType)
        {
            LicensePlate = licensePlate;
            ParkingTime = parkingTime;
            VehicleType = vehicleType;
        }
    }
}
