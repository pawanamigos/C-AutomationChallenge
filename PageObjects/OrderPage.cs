using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumC_.PageObjects
{
    public class OrderPage
    {
        private IWebDriver driver;
        public OrderPage(IWebDriver driver)
        {

            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }
        [FindsBy(How = How.ClassName, Using = "btn-primary")]
        private IWebElement OrderButton;

        public IWebElement getOrderButton()
        {
            return OrderButton;
        }
    }
}
