using NUnit.Framework;
using System;
using SampleSpecFLowTroubleshooting.UI;

namespace SampleSpecFLowTroubleshooting.Tests
{
    public class TestUtilities
    {
        private static string _testRunId = "";

        private static readonly string _testsBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        private static int _failCount = 0;

        public static string TestRunID
        {
            get
            {
                if (_testRunId == "")
                {
                    _testRunId = GetTimeStamp();
                }
                return _testRunId;
            }
        }

        public static string GetTimeStamp()
        {
            var date = DateTime.Now;
            return $"{date:yyMMdd_HHmmss}";
        }

        public static void FailTheTest(Exception ex, string message, string fileName = "Screenshot", int timeToWait = 0)
        {
            _failCount++;

            Console.WriteLine();
            Console.WriteLine($"EXCEPTION MESSAGE: {ex.Message}");
            Console.WriteLine($"FAIL MESSAGE: {message}");
            Console.WriteLine($"URL: {DriverHelpers.GetDriverUrl()}");

            var path = _testsBaseDirectory + @"\..\..\..\TestResults\Screenshots\";
            var screenshotName = Screenshot.TakeScreenshot(fileName, path, timeToWait);
            Console.WriteLine($"SCREENSHOT: {screenshotName}");
            message += "; see output for screenshot info.";

            Assert.True(false, message);
        }

        public static void FailTheTest(string message, bool takeScreenshot = false, string fileName = "Screenshot", int timeToWait = 0)
        {
            _failCount++;

            Console.WriteLine();
            Console.WriteLine($"FAIL MESSAGE: {message}");

            if (takeScreenshot)
            {
                var path = _testsBaseDirectory + @"\..\..\..\TestResults\Screenshots\";
                var screenshotName = Screenshot.TakeScreenshot(fileName, path, timeToWait);
                Console.WriteLine($"SCREENSHOT: {screenshotName}");
                message += "; see output for screenshot info.";
            }

            Assert.True(false, message);
        }

        public static void ReinitialDriverOnPreviousFail()
        {
            if (_failCount > 0)
            {
                Driver.Dispose();
                Driver.Initialize();
                _failCount = 0;
            }
        }

        public static void InitializeLog()
        {
            Console.WriteLine();
            Console.WriteLine($"TEST RUN ID: {TestRunID}");
            Console.WriteLine($"ENVIRONMENT: {Configuration.ENVIRONMENT}");
            Console.WriteLine($"HEADLESS BROWSING: {Configuration.HEADLESS_BROWSING.ToString()}");
            Console.WriteLine($"RESOLUTION: {Configuration.RESOLUTION}");
            Console.WriteLine();
        }

    }
}
