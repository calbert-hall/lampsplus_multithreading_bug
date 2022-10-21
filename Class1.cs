using Xunit;
using Applitools;
using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium;
using System.Net;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Xunit.Priority;
using Configuration = Applitools.Selenium.Configuration;
using System.Collections.Generic;

namespace Applitools_troubleshooting
{
    public class LampsPlus
    {
        //Base initialization class
        public class TestsBase : IDisposable
        {
            protected string testingEnv = string.Empty;
            private IWebDriver Driver { get; set; }
            //private Applitools.Appium.Eyes eyes { get; set; }

            private Applitools.Appium.Eyes _eyes;
            protected Applitools.Appium.Eyes Eyes => _eyes ?? (_eyes = new Applitools.Appium.Eyes());

            //protected Applitools.Selenium.Eyes Eyes = new Applitools.Selenium.Eyes();  //Toggle these comments to switch 

            public TestsBase()
            {
                //eyes = new Applitools.Appium.Eyes();
            }

            public void TestInialization()
            {
                Configuration config = new Configuration();
                Applitools.IConfiguration configVal = config.SetIgnoreDisplacements(true)
                    .SetApiKey("") //TODO set API key
                    .SetForceFullPageScreenshot(true).SetSaveDiffs(false).SetHideCaret(true);

                Eyes.SetConfiguration(configVal);

                Eyes.SaveDiffs = true;//Overrides baseline with the same name
                //Eyes.SendDom = false;//Mobile tests failed without this command (solution was provided by Applitools support).
                //Eyes.StitchMode = StitchModes.CSS;

                var date = DateTime.Now.ToString("MM.dd.yyyy");
                BatchInfo batchInfo = new BatchInfo("Lamps Plus");
                    //$"SdkTroubleshooting_{date}");
                batchInfo.Id = $"SdkTroubleshooting_Fri_1021_02";
                Eyes.Batch = batchInfo;

                ////LOCAL IPHONE RUN (SAFARI)
                //var options = new AppiumOptions();
                //options.AddAdditionalCapability("deviceName", "iPhone");
                //options.AddAdditionalCapability("automationName", "XCUITest");
                //options.AddAdditionalCapability("platformName", "ios");
                //options.AddAdditionalCapability("platformVersion", "15");
                //options.AddAdditionalCapability("browserName", "safari");
                //options.AddAdditionalCapability("udid", "auto");
                //options.AddAdditionalCapability("startIWDP", true); //This capability will let to start ios_webkit_debug_proxy programmatically on host Mac machine.
                //options.AddAdditionalCapability("xcodeOrgId", "xxxxxx");
                //options.AddAdditionalCapability("xcodeSigningId", "iPhone Developer");
                //options.AddAdditionalCapability("orientation", "PORTRAIT"); //Lock mobile orientation to Portrait
                //options.AddAdditionalCapability("autoAcceptAlerts", true);
                //options.AddAdditionalCapability("safariAllowPopups", true);
                //options.AddAdditionalCapability("safariInitialUrl", "http://appium.io/");
                //var remoteUri = "http://xxxxx:4444/wd/hub";
                //Driver = new IOSDriver<AppiumWebElement>(new Uri(remoteUri), options,
                // TimeSpan.FromMinutes(90));
                //Driver.Navigate().GoToUrl("https://www.lampsplus.com/products/havanese-dog-18-inch-square-throw-pillow__30n73.html");
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                //eyes.Open((IOSDriver<AppiumWebElement>)Driver, "LampsPlus", "testSDK");

                //REMOTE IPHONE RUN (SAUCE LABS CLOUD) -- Replicates on this!

                var options = new AppiumOptions();
                options.AddAdditionalCapability("username", ""); //TODO - Replace w/our credentials
                options.AddAdditionalCapability("accessKey", "");

                options.AddAdditionalCapability("platformName", "ios");
                options.AddAdditionalCapability("platformVersion", "15.7");

                options.AddAdditionalCapability("newCommandTimeout", 180);
                options.AddAdditionalCapability("autoAcceptAlerts", true);
                options.AddAdditionalCapability("name", "SDK_Cloud_test");


                options.AddAdditionalCapability("browserName", "Safari");
                options.AddAdditionalCapability("appium:deviceName", "iPhone Simulator");
                options.AddAdditionalCapability("appium:platformVersion", "15.4");
                options.AddAdditionalCapability("appium:automationName", "XCUITest");

                var sauceOptions = new Dictionary<string, object>();
                sauceOptions.Add("appiumVersion", "2.0.0-beta44");
                //sauceOptions.Add("build", "<your build id>");
                //sauceOptions.Add("name", "<your test name>");
                options.AddAdditionalCapability("sauce:options", sauceOptions);

                //options.AddAdditionalCapability("build", $"iOS_{date}");//If enabled allows to save tests under the build folders
                Driver = new IOSDriver<AppiumWebElement>(new Uri("https://applitools-dev:7f853c17-24c9-4d8f-a679-9cfde5b43951@ondemand.us-west-1.saucelabs.com:443/wd/hub"), options, TimeSpan.FromMinutes(90));
                Driver.Navigate().GoToUrl("https://www.lampsplus.com/products/havanese-dog-18-inch-square-throw-pillow__30n73.html");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                Eyes.Open((IOSDriver<AppiumWebElement>)Driver, "LampsPlus", "testSDK");


                ////LOCAL DESKTOP RUN (CHROME DRIVER) -- Could not replicate on this
                //var options = new ChromeOptions
                //{
                //    AcceptInsecureCertificates = true
                //};
                //options.PlatformName = "windows";
                //options.EnableMobileEmulation("iPhone 6/7/8");
                //Driver = new ChromeDriver(options);
                //Driver.Manage().Window.Maximize();
                //Driver.Navigate().GoToUrl("https://www.lampsplus.com/products/havanese-dog-18-inch-square-throw-pillow__30n73.html");
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;//@Brandon, You might not need this line as, most likely, Applitools server is whitelisted on your computer.
                //seleniumEyes.Open(Driver, "LampsPlus", "testSDK");



                ////REMOTE DESKTOP RUN (CHROME DRIVER, SELENIUM GRID)
                //var options = new ChromeOptions
                //{
                //    PlatformName = "LINUX",
                //    AcceptInsecureCertificates = true
                //};

                //var remoteUri = "http://xxxxxxx:4444/wd/hub";

                //Driver = new RemoteWebDriver(new Uri(remoteUri), options.ToCapabilities(), TimeSpan.FromMinutes(90));
                //Driver.Manage().Window.Maximize();
                //Driver.Navigate().GoToUrl("https://www.lampsplus.com/products/havanese-dog-18-inch-square-throw-pillow__30n73.html");

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;//@Brandon, You might not need this line as, most likely, Applitools server is whitelisted on your computer.

                //eyes.Open((RemoteWebDriver)Driver, "LampsPlus", "testSDK");

            }

            public void ApplitoolsCapture()
            {
                Eyes.CheckWindow("TestSdkError");
            }

            public void Dispose()
            {
                Driver?.Close();
                Driver?.Quit();

                if (Eyes != null)
                {
                    TestResults result = Eyes?.Close(false); //If Close() argument bool is "true", comparison test will fail if it fails on Applitools.

                    if (testingEnv == "A")//Delete baseline to have combined test result on Applitools dashboard for the test.
                    {
                        result.Delete();
                    }

                    Eyes.AbortIfNotClosed(); //Eyes method: If you call it after the test has been succesfully closed, then the call is ignored.         
                }
            }
        }

        [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
        public class VisualTestA : TestsBase
        {

            [SkippableTheory]
            [InlineData("A")]
            [InlineData("B")]
            public void TestA(string instance)
            {
                testingEnv = instance;
                TestInialization();
                ApplitoolsCapture();
            }
        }

        [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
        public class VisualTestB : TestsBase
        {

            [SkippableTheory]
            [InlineData("A")]
            [InlineData("B")]
            public void TestB(string instance)
            {
                testingEnv = instance;
                TestInialization();
                ApplitoolsCapture();
            }
        }

        [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
        public class VisualTestC : TestsBase
        {

            [SkippableTheory]
            [InlineData("A")]
            [InlineData("B")]
            public void TestC(string instance)
            {
                testingEnv = instance;
                TestInialization();
                ApplitoolsCapture();
            }
        }

        [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
        public class VisualTestD : TestsBase
        {

            [SkippableTheory]
            [InlineData("A")]
            [InlineData("B")]
            public void TestD(string instance)
            {
                testingEnv = instance;
                TestInialization();
                ApplitoolsCapture();
            }
        }

        [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
        public class VisualTestE : TestsBase
        {

            [SkippableTheory]
            [InlineData("A")]
            [InlineData("B")]
            public void TestE(string instance)
            {
                testingEnv = instance;
                TestInialization();
                ApplitoolsCapture();
            }
        }

        [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
        public class VisualTestF : TestsBase
        {

            [SkippableTheory]
            [InlineData("A")]
            [InlineData("B")]
            public void TestF(string instance)
            {
                testingEnv = instance;
                TestInialization();
                ApplitoolsCapture();
            }
        }

        [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
        public class VisualTestG : TestsBase
        {

            [SkippableTheory]
            [InlineData("A")]
            [InlineData("B")]
            public void TestG(string instance)
            {
                testingEnv = instance;
                TestInialization();
                ApplitoolsCapture();
            }
        }

        [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
        public class VisualTestH : TestsBase
        {

            [SkippableTheory]
            [InlineData("A")]
            [InlineData("B")]
            public void TestH(string instance)
            {
                testingEnv = instance;
                TestInialization();
                ApplitoolsCapture();
            }
        }

        [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
        public class VisualTestI : TestsBase
        {

            [SkippableTheory]
            [InlineData("A")]
            [InlineData("B")]
            public void TestI(string instance)
            {
                testingEnv = instance;
                TestInialization();
                ApplitoolsCapture();
            }
        }

        [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
        public class VisualTestO : TestsBase
        {

            [SkippableTheory]
            [InlineData("A")]
            [InlineData("B")]
            public void TestO(string instance)
            {
                testingEnv = instance;
                TestInialization();
                ApplitoolsCapture();
            }
        }

        [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
        public class VisualTest1 : TestsBase
        {

            [SkippableTheory]
            [InlineData("A")]
            [InlineData("B")]
            public void Test1(string instance)
            {
                testingEnv = instance;
                TestInialization();
                ApplitoolsCapture();
            }
        }

        [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
        public class VisualTest2 : TestsBase
        {

            [SkippableTheory]
            [InlineData("A")]
            [InlineData("B")]
            public void Test2(string instance)
            {
                testingEnv = instance;
                TestInialization();
                ApplitoolsCapture();
            }
        }

        [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
        public class VisualTest3 : TestsBase
        {

            [SkippableTheory]
            [InlineData("A")]
            [InlineData("B")]
            public void Test3(string instance)
            {
                testingEnv = instance;
                TestInialization();
                ApplitoolsCapture();
            }
        }
    }
}
