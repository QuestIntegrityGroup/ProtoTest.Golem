﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using ProtoTest.Golem.Core;
using ProtoTest.Golem.Purple.PurpleElements;


namespace ProtoTest.Golem.Purple
{
    public class PurpleTestBase : TestBase
    {
        //Used for logging how long it takes elements to appear.
        //Has to be set in the constructor for the testclass
        public static bool PerfLogging { get; set; }
        public static List<string> TestOutcomes = new List<string>();
        private string TestFileLoc;
        private string ProjectFile;

        public string TestFileLocation
        {
            get
            {
                if (TestFileLoc == null)
                {
                    SetFileInfo();
                }
                return TestFileLoc;
            }
        }
        public string ProjectFileName
        {
            get
            {
                if (ProjectFile == null)
                {
                    SetFileInfo();
                }
                return ProjectFile;
            }
        }

        private void SetFileInfo()
        {
            //TODO This is fugly - need a better solution
            TestFileLoc = @"C:\";
            ProjectFile = "NOT CONFIGURED";
            var machineName = Environment.MachineName;
            if (machineName == Config.Settings.purpleSettings.Machine1)
            {
                TestFileLoc = Config.Settings.purpleSettings.DataSetPath1;
                ProjectFile = Config.Settings.purpleSettings.ProjectName1;
            }
            if (machineName == Config.Settings.purpleSettings.Machine2)
            {
                TestFileLoc = Config.Settings.purpleSettings.DataSetPath2;
                ProjectFile = Config.Settings.purpleSettings.ProjectName2;
            }
            if (machineName == Config.Settings.purpleSettings.Machine3)
            {
                TestFileLoc = Config.Settings.purpleSettings.DataSetPath3;
                ProjectFile = Config.Settings.purpleSettings.ProjectName3;
            }
            if (machineName == Config.Settings.purpleSettings.Machine4)
            {
                TestFileLoc = Config.Settings.purpleSettings.DataSetPath4;
                ProjectFile = Config.Settings.purpleSettings.ProjectName4;
            }
        }
        
        public void TestSettings()
        {
            
        }

        [NUnit.Framework.SetUp]
        [MbUnit.Framework.SetUp]
        public void SetUp()
        {
            PurpleWindow.FindRunningProcess();
        }


        [NUnit.Framework.TearDown]
        [MbUnit.Framework.TearDown]
        public override void TearDownTestBase()
        {
            LogEvent(Common.GetCurrentTestName() + " " + TestContext.CurrentContext.Result.Status);
            PurpleWindow.EndProcess();

        }

       
        [NUnit.Framework.TestFixtureTearDown]
        public void FixtureTearDown()
        {
            
        }

        public void LogScreenshotIfTestFailed()
        {
            //Will need to rework logscreenshot if failed for NUnit
            //if(TestContext.CurrentContext.Outcome!=TestOutcome.Passed)
            //    TestLog.EmbedImage(null, PurpleWindow.purpleWindow.GetImage());
        }
    }
}