using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V123.Network;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumC_.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;
        public HomePage(IWebDriver driver) { 

            this.driver = driver;
            PageFactory.InitElements(driver, this);

        }

       [FindsBy(How = How.LinkText, Using = "Orders")]
       private IWebElement order;

       [FindsBy(How = How.Id, Using = "tableLabel")]
       private IWebElement tableLabel;

        public IWebElement getOrderLink()
        {
            return order;
        }

        public IWebElement getOrderText()
        {
            return tableLabel;
        }

    }
}
