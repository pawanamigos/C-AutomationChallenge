using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Configuration;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Core;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports;
using OpenQA.Selenium.DevTools.V123.Page;

namespace SeleniumC_.Utils
{
    public class BrowserSetup
    {

        public AventStack.ExtentReports.ExtentReports Reporter = new AventStack.ExtentReports.ExtentReports();
        public ExtentTest test;
        [OneTimeSetUp]

        public void ReportSetup()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            String reportPath = projectDirectory + "//TestReport.html";
            var htmlReport = new ExtentHtmlReporter(reportPath);
            Reporter = new AventStack.ExtentReports.ExtentReports();
            Reporter.AttachReporter(htmlReport);
            Reporter.AddSystemInfo("Reported by:", "Pavan Pamireddy");
         }

        public IWebDriver driver;
        [SetUp]
        public void StartBrowseBrowser()
        {
            test = Reporter.CreateTest(TestContext.CurrentContext.Test.Name);
            String browser = ConfigurationManager.AppSettings["browser"];
            SelectBrowser(browser);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);     //Implicit Wait foe Demo. Prefer to use Explicit as needed to save execution time.
            driver.Url = "https://localhost:44449/";
            driver.Manage().Window.FullScreen();

        }

        public IWebDriver getDriver()
        {
            return driver;
        }

        public void SelectBrowser(String BrowserName)
        {
            switch(BrowserName)
            {
                case "Chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;
                case "Firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    break;
                case "Edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                   break;

            }
        }

        public static JSONReader getJsonData()
        {
            return new JSONReader();
        }

        [TearDown]
        public void TearDown()
        {
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = TestContext.CurrentContext.Result.StackTrace;
            DateTime time = DateTime.Now;
            String filename = "Screenshot" + time.ToString("h_mm_ss") + ".png";
            if (testStatus == TestStatus.Failed)
            {
                test.Fail("Test Failed", CaptureScreenshot(driver, filename));
                test.Log(Status.Fail, "FailureTrace"+stackTrace);
            }
            else if (testStatus == TestStatus.Failed)
            {
                test.Pass("Test Passed");
            }
            driver.Dispose();
            //Reporter.Flush();
        }

        public MediaEntityModelProvider CaptureScreenshot(IWebDriver driver, String screenShotname)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotname).Build();
        }
    }
}
