using Microsoft.VisualStudio.TestTools.UnitTesting;
using pragueParkingV2.Core.Models;
using PragueParkingV2.Core.Services;

[TestClass]
public class ParkingGarageTests
{
    private ParkingGarage garage;

    [TestInitialize]
    public void Setup()
    {
        // Ladda konfigurationen från fil
        var configManager = new ConfigurationManager();
        var config = configManager.LoadConfig(); // Ladda config

        // Skapa en ny instans av ParkingGarage
        garage = new ParkingGarage(10, config, configManager); // Ändrad instansiering
    }


    [TestMethod]
    public void TestParkVehicle_Success()
    {
        var car = new Car("ABC123");
        bool result = garage.ParkVehicle(car);

        Assert.IsTrue(result, "Expected parking to succeed.");
        Assert.IsTrue(garage.GetAvailableSpots().Count() < 10, "Expected at least one spot to be occupied.");
    }

    [TestMethod]
    public void TestParkVehicle_NullVehicle()
    {
        // Försöker att parkerar ett null-fordon
        Assert.ThrowsException<ArgumentNullException>(() => garage.ParkVehicle(null), "Expected ArgumentNullException for null vehicle.");
    }

    [TestMethod]
    public void TestRemoveVehicle_Success()
    {
        var car = new Car("ABC123");
        garage.ParkVehicle(car); // Parkerar bilen först
        bool result = garage.RemoveVehicle("ABC123");

        Assert.IsTrue(result, "Expected to remove the vehicle.");
        Assert.AreEqual(10, garage.GetAvailableSpots().Count(), "Expected all spots to be available again.");
    }

    [TestMethod]
    public void TestRemoveVehicle_NotFound()
    {
        // Försökwe att ta bort ett fordon som inte finns
        bool result = garage.RemoveVehicle("XYZ789");

        Assert.IsFalse(result, "Expected removal to fail for non-existent vehicle.");
    }

    [TestMethod]
    public void TestDisplayGarageMap()
    {
        // Detta kan vara svårt att testa direkt eftersom det skriver till konsolen
        garage.DisplayGarageMap(); 
    }
}
