using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;

namespace SeleniumC_.PageObjects
{
    public class CreateOrderPage
    {
        private IWebDriver driver;
        public CreateOrderPage(IWebDriver driver)
        {

            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.ClassName, Using = "btn-primary")]
        private IWebElement CreateOrderButton;

        [FindsBy(How = How.Id, Using = "org-code")]
        private IWebElement Organization;


        [FindsBy(How = How.Id, Using = "site-id")]
        private IWebElement SiteOptions;

        [FindsBy(How = How.Id, Using = "modality")]
        private IWebElement Modality;

        [FindsBy(How = How.Id, Using = "mrn")]
        private IWebElement Mrn;

        [FindsBy(How = How.Id, Using = "first-name")]
        private IWebElement Firstname;

        [FindsBy(How = How.Id, Using = "last-name")]
        private IWebElement Lastname;

        [FindsBy(How = How.Id, Using = "accession-number")]
        private IWebElement Accession;

        [FindsBy(How = How.Id, Using = "study-date-time")]
        private IWebElement DateTime;

        [FindsBy(How = How.CssSelector, Using = "button[type='submit']")]
        private IWebElement Submit;

        public IWebElement getOrderButton()
        {
            return CreateOrderButton;
        }

        public IWebElement OrgOptions()
        {
            return Organization;
        }

        public IWebElement SiteOptns()
        {
            return SiteOptions;
        }

        public IWebElement ModalityOptions()
        {
            return Modality;
        }

        public IWebElement GetMrn()
        {
            return Mrn;
        }

        public IWebElement FirstName()
        {
            return Firstname;
        }

        public IWebElement LastName()
        {
            return Lastname;

        }

        public IWebElement AccessionNumber()
        {
            return Accession;

        }

        public IWebElement StudyDateTime()
        {
            return DateTime;

        }

        public IWebElement SubmitButton()
        {
            return Submit;


        }

        public void MakeOrder(String mrn, String firstname, String lastname, String accNo, String Orgname, String SiteID, String Modlality, String date, String time, String timeOfDay)
        {
            GetMrn().SendKeys(mrn);
            FirstName().SendKeys(firstname);
            LastName().SendKeys(lastname);
            AccessionNumber().SendKeys(accNo);
            SelectElement OrgSelection = new SelectElement(OrgOptions());
            OrgSelection.SelectByText(Orgname);
            SelectElement SiteSelection = new SelectElement(SiteOptns());
            SiteSelection.SelectByValue(SiteID);
            SelectElement ModalitySelection = new SelectElement(ModalityOptions());
            ModalitySelection.SelectByValue(Modlality);
            StudyDateTime().SendKeys(date);
            StudyDateTime().SendKeys(Keys.Tab);
            StudyDateTime().SendKeys(time);
            StudyDateTime().SendKeys(timeOfDay);
            SubmitButton().Click();            
        }
            
    }
}

