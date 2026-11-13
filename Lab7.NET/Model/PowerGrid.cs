using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.NET.Model
{
    public class PowerGrid : INotifiable
    {
        public event Action<string>? MessageGenerated;
        public int NotificationsCount { get; private set; }

        public void Notify(string message)
        {
            NotificationsCount++;
            MessageGenerated?.Invoke($"Електромережі: {message}");
        }

        public void OnStorm(double windSpeed) => Notify("Перейти в посилений режим");

        public void OnTemperatureDrop(double drop) =>
            Notify("Зростає навантаження. Підготувати резерв");
    }
}
