using System;
using System.Drawing;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SampleSpecFLowTroubleshooting.UI
{
    public class Driver
    {
        public static IWebDriver Instance { get; set; }

        private static string _windowSize
        {
            get
            {
                var resolution = Configuration.RESOLUTION;
                var windowSize = resolution.Replace(" x ", ",");
                return windowSize;
            }
        }

        public static void Initialize()
        {
            var options = new ChromeOptions();
            options.AddArgument($"--window-size={_windowSize}");
            if (Configuration.HEADLESS_BROWSING)
            {
                options.AddArgument("--headless");
            }
            Instance = new ChromeDriver(options);
            Instance.Manage().Window.Position = new Point(Configuration.X_COORDINATE, Configuration.Y_COORDINATE);
            DriverHelpers.SetWebDriverWait(Instance);
        }

        public static void Dispose()
        {
            Instance.Dispose();
        }

        internal static void Wait(int seconds = 1)
        {
            var timeSpan = TimeSpan.FromSeconds(seconds);
            Thread.Sleep((int)(timeSpan.TotalSeconds * 1000));
        }

        public static void NoWait(Action action)
        {
            TurnOffWait();
            action();
            TurnOnWait();
        }

        public static void TurnOnWait()
        {
            Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public static void TurnOffWait()
        {
            Instance.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }
    }
}

