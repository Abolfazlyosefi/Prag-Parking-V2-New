using Microsoft.VisualStudio.TestTools.UnitTesting;
using pragueParkingV2.Core.Models;

namespace pragueParkingV2.Tests
{
    [TestClass]
    public class VehicleTests
    {
        [TestMethod]
        public void Test_CalculateParkingFee()
        {
            var car = new Car("ABC123");
            car.ParkingTime = DateTime.Now.AddHours(-2); // Parkera 2 timmar tidigare

            var fee = car.CalculateParkingFee();

            Assert.AreEqual(40, fee); // 20 CZK per timme
        }
    }
}