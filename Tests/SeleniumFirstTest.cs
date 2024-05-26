using AngleSharp.Dom;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using System.Threading.Tasks;
using RestSharp;
using System.Net;
using SeleniumC_.Utils;
using SeleniumC_.PageObjects;
namespace SeleniumC_.Tests
{

    public class SeleniumFirstTest : BrowserSetup
    {
        [Test]

        public void checkTitle()
        {
            string title = driver.Title;
            TestContext.Progress.WriteLine(title);
            TestContext.Progress.WriteLine(driver.Url);
            Assert.AreEqual(title, "AutomationTestSample");
            driver.Close();
        }

        [Test]

        public void checkNoURLRedirect()
        {
            string url = driver.Url;
            TestContext.Progress.WriteLine(driver.Url);
            Assert.That(url, Is.EqualTo("https://localhost:44449/"));
            driver.Close();
        }

        [Test]
        public void checkUrlLinkText()
        {
            //IWebElement link = driver.FindElement(By.LinkText("Orders"));
            HomePage homePage = new HomePage(getDriver());
            Assert.That(homePage.getOrderLink().GetAttribute("href"), Is.EqualTo("https://localhost:44449/orders"));
        }

        [Test]

        public void NavigateToOrdersPage()
        {
            HomePage homePage = new HomePage(getDriver());
            homePage.getOrderLink().Click();
            Assert.That("Orders", Is.EqualTo(homePage.getOrderText().Text));
          }

        [Test]

        public void CreateNewOrderButtonEnabled()
        {
            HomePage homePage = new HomePage(getDriver());
            homePage.getOrderLink().Click();
            OrderPage OrderPage = new OrderPage(getDriver());
            Assert.True(OrderPage.getOrderButton().Enabled);
        }


        [Test]

        public void CheckAvailableOrgs()
        {
            string[] ExpectedOrgOptions = { "Lumus (LUM)", "Care UK (CUK)", "The Ultrasound Clinic (USC)" };
            HomePage homePage = new HomePage(getDriver());
            homePage.getOrderLink().Click();
            OrderPage OrderPage = new OrderPage(getDriver());
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("btn-primary")));
            OrderPage.getOrderButton().Click();
            CreateOrderPage CreateOrder = new CreateOrderPage(getDriver());
            SelectElement select = new SelectElement(CreateOrder.OrgOptions());
            IList<IWebElement> OrgOptions = select.Options;
            for (int j = 0; j < OrgOptions.Count; j++)
            {
                TestContext.Progress.WriteLine(OrgOptions[j].Text);
                Assert.That(OrgOptions[j].Text, Is.EqualTo(ExpectedOrgOptions[j]));
            }
        }

        [Test]

        public void CheckAvailableSites()
        {
            string[] ExpectedSiteOptions = { "Sussesx", "Lincoln", "Spalding" };
            HomePage homePage = new HomePage(getDriver());
            homePage.getOrderLink().Click();
            OrderPage OrderPage = new OrderPage(getDriver());
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("btn-primary")));
            OrderPage.getOrderButton().Click();
            CreateOrderPage CreateOrder = new CreateOrderPage(getDriver());
            SelectElement select = new SelectElement(CreateOrder.SiteOptns());
            IList<IWebElement> SiteOptions = select.Options;
            for (int j = 0; j < SiteOptions.Count; j++)
            {
                TestContext.Progress.WriteLine(SiteOptions[j].Text);
                Assert.That(SiteOptions[j].Text, Is.EqualTo(ExpectedSiteOptions[j]));
            }
        }

        [Test]

        public void CheckAvailableModality()
        {
            string[] ExpectedModalityOptions = { "MRI (MR)", "CT (CT)", "Xray (XR)", "Ultrasound (US)" };
            HomePage homePage = new HomePage(getDriver());
            homePage.getOrderLink().Click();
            OrderPage OrderPage = new OrderPage(getDriver());
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("btn-primary")));
            OrderPage.getOrderButton().Click();
            CreateOrderPage CreateOrder = new CreateOrderPage(getDriver());
            SelectElement select = new SelectElement(CreateOrder.ModalityOptions());
            IList<IWebElement> ModalityOptions = select.Options;
            for (int j = 0; j < ModalityOptions.Count; j++)
            {
                TestContext.Progress.WriteLine(ModalityOptions[j].Text);
                Assert.That(ModalityOptions[j].Text, Is.EqualTo(ExpectedModalityOptions[j]));
            }
        }

        [Test, TestCaseSource("AddTestDataConfig")]

        public void CreateOrder(String mrn, String firstname, String lastname, String accessionNo, String Orgname, String siteid, String modality, String date, String time, String timeofday)
        {
            HomePage homePage = new HomePage(getDriver());
            homePage.getOrderLink().Click();
            OrderPage OrderPage = new OrderPage(getDriver());
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("btn-primary")));
            OrderPage.getOrderButton().Click();
            CreateOrderPage CreateOrder = new CreateOrderPage(getDriver());
            homePage.getOrderLink().Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//td[normalize-space()='John Wood']")));
            Assert.That(driver.FindElement(By.XPath("//td[normalize-space()='John Wood']")).Text, Is.EqualTo("John Wood"));
        }

        [Test]
        public async Task CheckPatientsAPIStatusCode()
        {
            var options = new RestClientOptions("https://localhost:44449/api/Patients");
            var client = new RestClient(options);
            var request = new RestRequest("https://localhost:44449/api/Patients", Method.Get);
            var response = await client.GetAsync(request);
            TestContext.Progress.WriteLine(response.Content);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task CheckOrdersAPIStatusCode()
        {
            var options = new RestClientOptions("https://localhost:44449/api/Patients");
            var client = new RestClient(options);
            var request = new RestRequest("https://localhost:44449/api/Orders", Method.Get);
            var response = await client.GetAsync(request);
            TestContext.Progress.WriteLine(response.Content);
        }

        //Parameterize Test Data. 
        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {
            yield return new TestCaseData(getJsonData().ExtractData("mrn"), getJsonData().ExtractData("firstname"), getJsonData().ExtractData("lastname"), getJsonData().ExtractData("accessionNo"), getJsonData().ExtractData("orgname"), getJsonData().ExtractData("siteid"), getJsonData().ExtractData("modality"), getJsonData().ExtractData("date"), getJsonData().ExtractData("time"), getJsonData().ExtractData("timeofday"));
        }
    }

}
