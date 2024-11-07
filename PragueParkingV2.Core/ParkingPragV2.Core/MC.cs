namespace pragueParkingV2.Core.Models
{
    public class Motorcycle : Vehicle
    {
        public Motorcycle(string licensePlate) : base(licensePlate) { }

        public override decimal CalculateParkingFee()
        {
            var totalTimeParked = (DateTime.Now - ParkingTime).TotalMinutes;
            if (totalTimeParked <= FreeMinutes) return 0;

            var hoursParked = (decimal)Math.Ceiling((totalTimeParked - FreeMinutes) / 60);
            return hoursParked * 10M; // 20 CZK per timme
        }


    }
}
