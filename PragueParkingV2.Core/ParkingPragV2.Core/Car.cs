namespace pragueParkingV2.Core.Models
{
    
    public class Car : Vehicle
    {
        // Konstruktör för Car som tar registreringsnummer
        public Car(string licensePlate) : base(licensePlate) { }

        // Beräknar parkeringsavgiften
        public override decimal CalculateParkingFee()
        {
            // Beräknar tiden som bilen har varit parkerad
            var totalTimeParked = (DateTime.Now - ParkingTime).TotalMinutes;

            // Returnerar 0 om tiden är inom de fria minuterna
            if (totalTimeParked <= FreeMinutes) return 0;

            // Beräknar antal timmar efter de fria minuterna
            var hoursParked = (decimal)Math.Ceiling((totalTimeParked - FreeMinutes) / 60);

            // Returnerar avgiften (20 CZK per timme)
            return hoursParked * 20M;
        }
    }
}
