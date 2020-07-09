using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace SampleSpecFLowTroubleshooting.UI
{
    public static class DriverHelpers
    {
        private static WebDriverWait _wait;

        public static void SetWebDriverWait(IWebDriver driver)
        {
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public static string GetDriverUrl() => Driver.Instance.Url;

        public static void WaitForDOMLoadComplete(int timeoutSeconds = 30)
        {
            WaitForStableDOM(timeoutSeconds);
        }

        private static void WaitForStableDOM(int timeoutSeconds = 30)
        {
            //wait for the page source to stop changing
            var timeoutThreshold = timeoutSeconds;
            var startTime = DateTime.Now;
            while (DateTime.Now - startTime < TimeSpan.FromSeconds(timeoutThreshold))
            {
                var prevState = Driver.Instance.PageSource;
                Thread.Sleep(TimeSpan.FromMilliseconds(500)); // <-- would need to wrap in a try catch??
                if (prevState == Driver.Instance.PageSource)
                {
                    return;
                }
            }
            Console.WriteLine();
            Console.WriteLine($"TIMEOUT: WaitForStableDom() method timed out after {timeoutThreshold} seconds");
        }

        public static void WaitAndClick(IWebElement element)
        {
            WaitForClickable(element);
            Click(element);
        }

        public static void WaitAndClick(By by)
        {
            var element = Driver.Instance.FindElement(by);
            WaitAndClick(element);
        }

        public static void WaitAndMoveToAndClick(By by)
        {
            WaitForExists(by);
            MoveTo(by);
            Click(by);
        }

        public static void WaitAndScrollAndClick(IWebElement element)
        {
            WaitForClickable(element);
            ScrollToAndClick(element);
        }

        public static void WaitForClickable(IWebElement element, int timeoutSeconds = 10)
        {
            //selenium will throw an exception if element is not found
            _wait.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementToBeClickable(element));
        }

        public static void WaitForClickable(By by, int timeoutSeconds = 10)
        {
            //selenium will throw an exception if element is not found
            WaitForClickable(Driver.Instance.FindElement(by), timeoutSeconds);
        }

        public static void WaitForExists(By by, int timeoutSeconds = 10)
        {
            //selenium will throw an exception if element is not found
            _wait.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementExists(by));
        }

        public static void WaitForVisible(string xPath, int timeoutSeconds = 10)
        {
            _wait.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                    .ElementIsVisible(By.XPath(xPath)));
        }

        public static void WaitForVisible(string id)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(By.Id(id)));
        }

        public static void WaitForVisible(By by, int timeoutSeconds = 10)
        {
            _wait.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .ElementIsVisible(by));
        }

        public static void WaitForNotVisible(string xPath)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions
                .InvisibilityOfElementLocated(By.XPath($"{xPath}")));
        }

        public static void ScrollTo(IWebElement element)
        {
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public static void ScrollToEnd()
        {
            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        public static void ScrollToAndClick(IWebElement element)
        {
            ScrollTo(element);
            Click(element);
        }

        public static void ScrollToAndClick(By by)
        {
            ScrollToAndClick(Driver.Instance.FindElement(by));
        }

        public static void Click(IWebElement element)
        {
            element.Click();
        }

        public static void Click(By by)
        {
            Driver.Instance.FindElement(by).Click();
        }

        public static void MoveTo(IWebElement element)
        {
            Actions action = new Actions(Driver.Instance);
            action.MoveToElement(element).Build().Perform();
        }

        public static void MoveTo(By by)
        {
            MoveTo(Driver.Instance.FindElement(by));
        }

        public static void MoveToAndClick(IWebElement element)
        {
            MoveTo(element);
            Click(element);
        }

         public static void MoveToAndClickAtElementLocation(IWebElement element)
         {
            new Actions(Driver.Instance).MoveToElement(element).MoveByOffset(0, 0).Click().Perform();
         }

        public static void ScrollElementIntoCenterOfViewPort(IWebElement element)
        {
            string script = "var viewPortHeight = Math.max(document.documentElement.clientHeight, window.innerHeight || 0);"
                                                        + "var elementTop = arguments[0].getBoundingClientRect().top;"
                                                        + "window.scrollBy(0, elementTop-(viewPortHeight/2));";

            ((IJavaScriptExecutor)Driver.Instance).ExecuteScript(script, element);
        }

        public static bool VerifyElementClickable(IWebElement element, string description)
        {
            try
            {
                WaitForClickable(element);
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine($"ELEMENT VERIFICATION FAILED: {description} was not found or not clickable");
                return false;
            }
            return true;
        }

        public static bool VerifyElementClickable(By by, string description)
        {
            try
            {
                WaitForClickable(Driver.Instance.FindElement(by));
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine($"ELEMENT VERIFICATION FAILED: {description} was not found or not clickable");
                Console.WriteLine($"Method attempted: {by.ToString()}");
                return false;
            }
            return true;
        }

        public static bool VerifyElementNotClickable(IWebElement element, string description, int timeoutSeconds = 10)
        {
            if (element != null)
            {
                try
                {
                    WaitForClickable(element, timeoutSeconds);
                }
                catch
                {
                    return true;
                }
                Console.WriteLine();
                Console.WriteLine($"ELEMENT WAS CLICKABLE IN ERROR: {description} was present and clickable but not expected to be clickable");
                return false;
            }
            Console.WriteLine();
            Console.WriteLine($"ELEMENT NOT FOUND: {description} was not present at all");
            return false;
        }

        public static bool VerifyNotElement(IWebElement element, string description)
        {
            if (element != null)
            {
                try
                {
                    WaitForClickable(element);
                }
                catch
                {
                    return true;
                }
                Console.WriteLine();
                Console.WriteLine($"ELEMENT FOUND IN ERROR: {description} was present and clickable but not expected to be present");
                return false;
            }
            return true;
        }

        public static bool ElementExists(By by)
        {

            var elements = Driver.Instance.FindElements(by);
            return elements.Count == 1;
        }

        public static void ZoomInOut(int level)
        {
            ((IJavaScriptExecutor)Driver.Instance)
                .ExecuteScript(string.Format("document.body.style.zoom='{0}%'", level));
        }
    }
}
