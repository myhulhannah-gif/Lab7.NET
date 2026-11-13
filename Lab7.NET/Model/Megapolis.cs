using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.NET.Model
{
    public class Megapolis : City
    {
        public int Districts { get; set; }

        public Megapolis(string name, int population, int districts)
            : base(name, population)
        {
            Districts = districts;
        }

        public override string ToString() =>
            $"Мегаполіс {Name}, населення {Population}, районів {Districts}";
    }
}
