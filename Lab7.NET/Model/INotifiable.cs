using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.NET.Model
{
    public interface INotifiable
    {
        event Action<string>? MessageGenerated;
        void Notify(string message);
    }
}
