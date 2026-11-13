using Lab7.NET.Model;

namespace Lab7.NET.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void StormWarning_Fires_WhenWindAbove25()
        {
            var station = new WeatherStation(0);
            bool fired = false;
            station.StormWarning += w => fired = true;

            station.SetWind(30);

            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void StormWarning_DoesNotFire_WhenWindBelow25()
        {
            var station = new WeatherStation(0);
            bool fired = false;
            station.StormWarning += w => fired = true;

            station.SetWind(10);

            Assert.IsFalse(fired);
        }

        [TestMethod]
        public void StormWarning_NotifiesAllThreeSubscribers()
        {
            var station = new WeatherStation(0);
            var dsns = new EmergencyService();
            var admin = new CityAdministration();
            var grid = new PowerGrid();
            station.StormWarning += dsns.OnStorm;
            station.StormWarning += admin.OnStorm;
            station.StormWarning += grid.OnStorm;

            station.SetWind(30);

            Assert.AreEqual(1, dsns.NotificationsCount);
            Assert.AreEqual(1, admin.NotificationsCount);
            Assert.AreEqual(1, grid.NotificationsCount);
        }

        [TestMethod]
        public void TemperatureDrop_Fires_WhenDropIs10OrMore()
        {
            var station = new WeatherStation(5);
            double receivedDrop = 0;
            station.TemperatureDrop += d => receivedDrop = d;

            station.SetTemperature(-8);

            Assert.AreEqual(13.0, receivedDrop);
        }

        [TestMethod]
        public void HeavyRain_Fires_WhenRainAbove30()
        {
            var station = new WeatherStation(0);
            bool fired = false;
            station.HeavyRain += mm => fired = true;

            station.SetRain(45);

            Assert.IsTrue(fired);
        }

        [TestMethod]
        public void WindCalmed_Fires_AfterStorm()
        {
            var station = new WeatherStation(0);
            bool calmed = false;
            station.WindCalmed += () => calmed = true;

            station.SetWind(30);
            station.SetWind(5);

            Assert.IsTrue(calmed);
        }

        [TestMethod]
        public void MulticastDelegate_CallsAllThreeMethods()
        {
            var city = new City("Тест", 100);
            Action plan = city.OpenShelters;
            plan += city.SendAlert;
            plan += city.LightsOn;

            plan();

            Assert.AreEqual(3, city.ActionsDone);
        }

        [TestMethod]
        public void Megapolis_IsCity()
        {
            var m = new Megapolis("Київ", 1000, 10);

            Assert.IsInstanceOfType(m, typeof(City));
        }

        [TestMethod]
        public void EmergencyService_ImplementsThreeInterfaces()
        {
            var dsns = new EmergencyService();

            Assert.IsInstanceOfType(dsns, typeof(INotifiable));
            Assert.IsInstanceOfType(dsns, typeof(IRespondable));
            Assert.IsInstanceOfType(dsns, typeof(IReportable));
        }
    }
}