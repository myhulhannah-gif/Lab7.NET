using Lab7.NET.Model;
using System;
using System.Threading.Tasks;

public class EmergencyService : INotifiable, IRespondable, IReportable
{
    public event Action<string>? MessageGenerated;
    public int NotificationsCount { get; private set; }

    public void Notify(string message)
    {
        NotificationsCount++;
        MessageGenerated?.Invoke($"ДСНС: {message}");
    }

    public void Respond() => MessageGenerated?.Invoke("ДСНС: Бригади виїхали на місце");

    public string GetReport() => $"ДСНС отримала сповіщень: {NotificationsCount}";

    public async void OnStorm(double windSpeed)
    {
        Notify($"Вітер {windSpeed} м/с. Підготувати бригади");
        await Task.Delay(2000);
        Respond();
    }

    public void OnHeavyRain(double mm) => Notify($"Злива {mm} мм. Відкачати воду з вулиць");

    public async void OnWindCalmed()
    {
        Notify("Вітер вщух. Відбій тривоги");
        await Task.Delay(2000);
        MessageGenerated?.Invoke("ДСНС: Наслідки шторму ліквідовано");
    }
}