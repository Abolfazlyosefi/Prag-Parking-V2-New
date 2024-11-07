using Microsoft.VisualStudio.TestTools.UnitTesting;
using pragueParkingV2.Core.Models;
using PragueParkingV2.Core.Services;

namespace pragueParkingV2.Tests
{
    [TestClass]
    public class ParkingGarageTests
    {
        private ConfigurationManager configManager; // Lägg till denna rad
        private ConfigData configData; // Lägg till denna rad

        [TestInitialize]
        public void Setup()
        {
            configManager = new ConfigurationManager(); // Initiera ConfigurationManager
            configData = configManager.LoadConfig(); // Ladda konfigurationen
        }

        [TestMethod]
        public void TestParkVehicle()
        {
            var configManager = new ConfigurationManager();
            var config = configManager.LoadConfig(); // Ladda config

            var garage = new ParkingGarage(100, config, configManager); // Ändrad instansiering
            var car = new Car("ABC123");

            var result = garage.ParkVehicle(car);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestRemoveVehicle()
        {
            var configManager = new ConfigurationManager();
            var config = configManager.LoadConfig(); // Ladda config

            var garage = new ParkingGarage(100, config, configManager); // Ändrad instansiering
            var car = new Car("ABC123");

            garage.ParkVehicle(car);
            var result = garage.RemoveVehicle("ABC123");

            Assert.IsTrue(result);
        }

    }
}
