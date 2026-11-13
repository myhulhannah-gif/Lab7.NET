using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.NET.Model
{
    // Делегати
    public delegate void StormHandler(double windSpeed);
    public delegate void TemperatureHandler(double drop);
    public delegate void RainHandler(double mm);
    public delegate void CalmHandler();

    public class WeatherStation
    {
        public event StormHandler? StormWarning;           // широкомовна
        public event TemperatureHandler? TemperatureDrop;  // широкомовна
        public event RainHandler? HeavyRain;               // широкомовна
        public event CalmHandler? WindCalmed;
        public event Action<string>? MessageGenerated;

        private double _wind;
        private double _temperature;

        public WeatherStation(double startTemperature)
        {
            _temperature = startTemperature;
        }

        public void SetWind(double speed)
        {
            bool wasStorm = _wind > 25;
            _wind = speed;
            MessageGenerated?.Invoke($"Метеостанція: Вітер {speed} м/с");

            if (speed > 25)
                StormWarning?.Invoke(speed);
            else if (wasStorm)
                WindCalmed?.Invoke();
        }

        public void SetTemperature(double newTemperature)
        {
            double drop = _temperature - newTemperature;
            _temperature = newTemperature;
            MessageGenerated?.Invoke($"Метеостанція: Температура {newTemperature}°");

            if (drop >= 10)
                TemperatureDrop?.Invoke(drop);
        }

        public void SetRain(double mm)
        {
            MessageGenerated?.Invoke($"Метеостанція: Опади {mm} мм");

            if (mm > 30)
                HeavyRain?.Invoke(mm);
        }
    }
}
