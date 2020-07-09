using NUnit.Framework;
using SampleSpecFLowTroubleshooting.UI;
using TechTalk.SpecFlow;

namespace SampleSpecFLowTroubleshooting.Tests.Steps
{
    [Binding]
    public class GoogleSiteSteps
    {
        [When(@"I navigate to (.*)")]
        public void WhenINavigateTo(string url)
        {
            Assert.True(GooglePage.GoTo(), "went to google");
        }
        
        [Then(@"I am at (.*)")]
        public void ThenIAmAt(string url)
        {
            Assert.True(GooglePage.IsAt);
        }
    }
}
