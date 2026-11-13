using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.NET.Model
{
    public class CityAdministration : INotifiable
    {
        public event Action<string>? MessageGenerated;
        public int NotificationsCount { get; private set; }

        public void Notify(string message)
        {
            NotificationsCount++;
            MessageGenerated?.Invoke($"Мерія: {message}");
        }

        public void OnStorm(double windSpeed) => Notify("Розіслати SMS громадянам");

        public void OnTemperatureDrop(double drop) =>
            Notify($"Похолодання на {drop}°. Відкрити пункти обігріву");

        public void OnHeavyRain(double mm) => Notify($"Злива {mm} мм. Попередити громадян");
    }
}
