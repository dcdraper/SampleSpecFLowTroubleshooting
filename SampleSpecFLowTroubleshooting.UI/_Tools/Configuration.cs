using System;

namespace SampleSpecFLowTroubleshooting.UI
{
    public class Configuration
    {

        //EXECUTION SETTINGS////////////////////////////////////////////////////////////////

        public static string ENVIRONMENT = "QA"; //Staging, QA, Dev, Prod

        public static bool HEADLESS_BROWSING = false;

        public static string RESOLUTION = "1920 x 1920"; //"1120 x 1080"; //"800 x 1800"; 

        public static int X_COORDINATE = 1;
        public static int Y_COORDINATE = 1;

        //CONFIG////////////////////////////////////////////////////////////////////////////
        public static string AutomationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

    }
}
