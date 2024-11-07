namespace pragueParkingV2.Core.Services
{
    public class PriceConfigData
    {
        public decimal CarRate { get; set; } = 20M; // CZK per hour
        public decimal MotorcycleRate { get; set; } = 10M; // CZK per hour
        public int FreeMinutes { get; set; } = 10; // Free minutes
    }
}
