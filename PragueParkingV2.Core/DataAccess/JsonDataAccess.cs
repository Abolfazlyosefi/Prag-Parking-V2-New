using Newtonsoft.Json;
using pragueParkingV2.Core.Models;

namespace pragueParkingV2.DataAccess
{
    public class JsonDataAccess
    {
        // Konstant för namnet på JSON-filen som lagrar parkeringsdata
        private const string ParkingDataFile = "parking_data.json";

        // Laddar parkeringsdata från JSON-filen och returnerar en lista av ParkingSpot-objekt
        public List<ParkingSpot> LoadParkingData()
        {
            // Kollar om JSON-filen finns; om inte, returnera en tom lista
            if (!File.Exists(ParkingDataFile))
                return new List<ParkingSpot>();

            // Läser innehållet i JSON-filen
            var json = File.ReadAllText(ParkingDataFile);

            // Deserialiserar JSON-data till en lista av ParkingSpot-objekt och returnerar den
            return JsonConvert.DeserializeObject<List<ParkingSpot>>(json);
        }

        // Sparar listan av ParkingSpot-objekt till JSON-filen
        public void SaveParkingData(List<ParkingSpot> parkingSpots)
        {
            // Serialiserar listan av ParkingSpot-objekt till en JSON-sträng med indenterad formatering
            var json = JsonConvert.SerializeObject(parkingSpots, Formatting.Indented);

            // Skriver JSON-strängen till den angivna filen, och överskriver eventuell befintlig innehåll
            File.WriteAllText(ParkingDataFile, json);
        }
    }
}
