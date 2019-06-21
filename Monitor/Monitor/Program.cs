using System;
using System.IO;
using Monitor.Models;
using Monitor.Services;
using Newtonsoft.Json;
using Timer = System.Timers.Timer;

namespace Monitor
{
    class Program
    {
        public static Timer Timer { get; set; }

        public static Settings Settings { get; set; }

        public static UpdateTemperatureService Service { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Monitoring citties");
            Initialize();
            Timer.Start();
            Console.WriteLine("Type any key to cancel");
            Console.ReadKey();
        }

        static void Initialize()
        {
            var settingText = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "settings.json"));

            Settings = JsonConvert.DeserializeObject<Settings>(settingText);

            Timer = new Timer
            {
                Interval = Settings.Interval //change setting.json to interval in milliseconds
            };

            Service = new UpdateTemperatureService(Settings.ApiSettings);

            Timer.Elapsed += (sender, e) =>
            {
                Timer.Stop();
                Console.WriteLine("Starting update");
                Service.UpdateTemperatures();
                Console.WriteLine("Update completed");
                Timer.Start();
            };
        }
    }
}
