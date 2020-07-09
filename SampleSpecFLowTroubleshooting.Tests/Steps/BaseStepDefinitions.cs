using SampleSpecFLowTroubleshooting.UI;
using TechTalk.SpecFlow;

namespace SampleSpecFLowTroubleshooting.Tests
{
    [Binding]
    public class BaseStepDefinitions
    {
        //BEFORE TEST RUN///////////////////////////////////////////////////////////////////////
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            TestUtilities.InitializeLog();
            Driver.Initialize();
        }

        //AFTER TEST RUN////////////////////////////////////////////////////////////////////////
        [AfterTestRun]
        public static void AfterTestRun()
        {
            Driver.Dispose();
        }


        ////BEFORE FEATURE////////////////////////////////////////////////////////////////////////
        //[BeforeFeature]
        //public static void BeforeFeature()
        //{
        //    //Driver.Initialize();
        //}

        ////AFTER FEATURE/////////////////////////////////////////////////////////////////////////
        //[AfterFeature]
        //public static void AfterFeature()
        //{
        //    //Driver.Dispose();
        //}


        ////BEFORE SCENARIO///////////////////////////////////////////////////////////////////////
        //[BeforeScenario]
        //public static void BeforeScenario()
        //{
        //    TestUtilities.ReinitialDriverOnPreviousFail();

        //}

        ////AFTER SCENARIO////////////////////////////////////////////////////////////////////////
        //[AfterScenario]
        //public static void AfterScenario()
        //{
        //}


        ////BEFORE SCENARIO BLOCK/////////////////////////////////////////////////////////////////
        //[BeforeScenarioBlock]
        //public static void BeforeScenarioBlock()
        //{
        //}

        ////AFTER SCENARIO BLOCK//////////////////////////////////////////////////////////////////
        //[AfterScenarioBlock]
        //public static void AfterScenarioBlock()
        //{
        //}


        ////BEFORE STEP//////////////////////////////////////////////////////////////////////////
        //[BeforeStep]
        //public static void BeforeStep()
        //{
        //}

        ////AFTER STEP//////////////////////////////////////////////////////////////////////////
        //[AfterStep]
        //public static void AfterStep()
        //{
        //}
    }
}
