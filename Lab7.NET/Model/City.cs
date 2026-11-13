using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.NET.Model
{
    public class City
    {
        public event Action<string>? MessageGenerated;

        public string Name { get; set; }
        public int Population { get; set; }
        public int ActionsDone { get; private set; }

        public City(string name, int population)
        {
            Name = name;
            Population = population;
        }

        public void OpenShelters()
        {
            ActionsDone++;
            MessageGenerated?.Invoke($"{Name}: Відкрито укриття");
        }

        public void SendAlert()
        {
            ActionsDone++;
            MessageGenerated?.Invoke($"{Name}: Надіслано сповіщення мешканцям");
        }

        public void LightsOn()
        {
            ActionsDone++;
            MessageGenerated?.Invoke($"{Name}: Увімкнено аварійне освітлення");
        }

        public override string ToString() => $"Місто {Name}, населення {Population}";
    }
}
