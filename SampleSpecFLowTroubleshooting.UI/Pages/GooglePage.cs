namespace SampleSpecFLowTroubleshooting.UI
{
    public class GooglePage
    {
        private static readonly string url = "https://www.google.com";
        public static bool IsAt => Driver.Instance.Url.Contains(url);

        public static bool GoTo()
        {
            Driver.Instance.Navigate().GoToUrl(url);
            return IsAt;
        }
    }
}
