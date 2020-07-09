using System;
using System.IO;
using OpenQA.Selenium;

namespace SampleSpecFLowTroubleshooting.UI
{
    public class Screenshot
    {
        private static int _screenshotCounter = 0;

        public static string TakeScreenshot(string fileName, string path, int timeToWait = 0)
        {
            Driver.Wait(timeToWait);
            _screenshotCounter++; //Updates the number of screenshots that we took during the execution
            var timeAndDate = DateTime.Now;
            var screenshotName =
                $"{path}{string.Format("{0:000}", _screenshotCounter)}_{string.Format("{0:MM.dd.yyyy_HH.mm.ss}", timeAndDate)}_{fileName}.jpeg";
            var validation = new DirectoryInfo(path); //System IO object
            if (validation.Exists == true) //Capture screen if the path is available
            {
                ((ITakesScreenshot)Driver.Instance).GetScreenshot().SaveAsFile(screenshotName, OpenQA.Selenium.ScreenshotImageFormat.Jpeg);
            }
            else //Create the folder and then Capture the screen
            {
                validation.Create();
                ((ITakesScreenshot)Driver.Instance).GetScreenshot().SaveAsFile(screenshotName, OpenQA.Selenium.ScreenshotImageFormat.Jpeg);
            }
            return screenshotName;
        }
    }
}

