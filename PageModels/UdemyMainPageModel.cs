using OpenQA.Selenium;
using System;

namespace Sysmap_udemy_test.PageModels
{
    internal class UdemyMainPageModel
    {
        private IWebElement udemyHeader;

        private IWebElement udemyLogoButton;

        private IWebElement searchBar;

        private IWebElement searchButton;

        public UdemyMainPageModel(IWebDriver webDriver)
        {
            // init elements in here
        }

        public void SearchByKeyword(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                throw new ArgumentNullException("keyword");
            }

            if (searchBar == null)
            {
                throw new Exception("Udemy main page was not loaded properly");
            }

            searchBar.SendKeys(keyword);

            searchButton.Click();
        }
    }
}
