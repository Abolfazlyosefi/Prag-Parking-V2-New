namespace PragueParkingV2.Core.Services
{
    public class ConfigData
    {
        public int TotalParkingSpots { get; set; } // Totalt antal parkeringsplatser
        public Dictionary<string, int>? VehicleTypes { get; set; } // Typ av fordon (nullable)
        public int FreeMinutes { get; set; } // Antal gratis minuter

        // Laddar prissättningsdata via ConfigurationManager
        public Dictionary<string, int> LoadPricing(ConfigurationManager configManager)
        {
            return configManager.LoadPricingConfig(); // Anropar metoden för att ladda prissättningen
        }
    }
}
