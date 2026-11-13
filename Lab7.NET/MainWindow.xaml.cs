using Lab7.NET.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab7.NET
{
    public partial class MainWindow : Window
    {
        private readonly Megapolis city;
        private readonly WeatherStation station;
        private readonly EmergencyService dsns;
        private readonly CityAdministration admin;
        private readonly PowerGrid grid;

        public MainWindow()
        {
            InitializeComponent();

            // 1. Створюємо об'єкти
            city = new Megapolis("Київ", 3_000_000, 10);
            station = new WeatherStation(5);
            dsns = new EmergencyService();
            admin = new CityAdministration();
            grid = new PowerGrid();

            // 2. Підписка на події метеостанції (+= додає метод у список події)
            station.StormWarning += dsns.OnStorm;
            station.StormWarning += admin.OnStorm;
            station.StormWarning += grid.OnStorm;

            station.TemperatureDrop += admin.OnTemperatureDrop;
            station.TemperatureDrop += grid.OnTemperatureDrop;

            station.HeavyRain += dsns.OnHeavyRain;
            station.HeavyRain += admin.OnHeavyRain;

            station.WindCalmed += dsns.OnWindCalmed;

            // 3. Підписка вікна на повідомлення (щоб бачити текст у журналі)
            station.MessageGenerated += LogMessage;
            dsns.MessageGenerated += LogMessage;
            admin.MessageGenerated += LogMessage;
            grid.MessageGenerated += LogMessage;
            city.MessageGenerated += LogMessage;
        }

        private void btnWind_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtWind.Text, out double wind))
            {
                LogMessage("Введіть число для швидкості вітру.");
                return;
            }
            if (wind < 0 || wind > 100)
            {
                LogMessage("Швидкість вітру має бути від 0 до 100 м/с.");
                return;
            }
            station.SetWind(wind);
        }

        private void btnTemp_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtTemp.Text, out double t))
            {
                LogMessage("Введіть число для температури.");
                return;
            }
            if (t < -60 || t > 60)
            {
                LogMessage("Температура має бути від -60 до 60 °C.");
                return;
            }
            station.SetTemperature(t);
        }

        private void btnRain_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(txtRain.Text, out double mm))
            {
                LogMessage("Введіть число для кількості опадів.");
                return;
            }
            if (mm < 0 || mm > 500)
            {
                LogMessage("Кількість опадів має бути від 0 до 500 мм.");
                return;
            }
            station.SetRain(mm);
        }

        // Багатоадресний делегат
        private void btnPlan_Click(object sender, RoutedEventArgs e)
        {
            Action plan = city.OpenShelters;   // перший метод
            plan += city.SendAlert;            // другий
            plan += city.LightsOn;             // третій
            plan();                            // виконуються всі три
        }

        private void LogMessage(string message)
        {
            var item = new ListBoxItem();
            string time = DateTime.Now.ToString("HH:mm:ss");

            if (message.StartsWith("Метеостанція"))
            {
                item.Content = $"[{time}] {message}";
                item.FontWeight = FontWeights.Bold;
                item.Background = new SolidColorBrush(Color.FromRgb(235, 235, 235));
                item.Margin = new Thickness(0, 8, 0, 0);
            }
            else
            {
                item.Content = $"      [{time}] {message}";
            }

            logListBox.Items.Add(item);
            logListBox.ScrollIntoView(item);
        }
    }
}