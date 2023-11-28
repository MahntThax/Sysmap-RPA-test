using OpenQA.Selenium;
using Sysmap_udemy_test.Utilities;
using System;

namespace Sysmap_udemy_test.PageModels
{
    internal class UdemyMainPageModel
    {
        private By udemyHeaderElementSelect = By.CssSelector("div.ud-app-loader:nth-child(1)");
        private IWebElement udemyHeader;

        private By udemyLogoButtonElementSelect = By.ClassName("desktop-header-module--flex-middle--1e7c8 desktop-header-module--logo--2Qf0r");
        private IWebElement udemyLogoButton;

        private By searchBarElementSelect = By.Id("u167-search-form-autocomplete--3");
        private IWebElement searchBar;

        private By searchButtonElementSelect = By.CssSelector("button.ud-btn-ghost:nth-child(3)");
        private IWebElement searchButton;

        public UdemyMainPageModel(IWebDriver webDriver)
        {
            // init elements in here
            udemyHeader = SeleniumUtilities.ReturnElement(webDriver, udemyHeaderElementSelect);

            if (udemyHeader == null)
            {
                throw new Exception("Udemy main page was not loaded properly");
            }

            udemyLogoButton = SeleniumUtilities.ReturnElement(udemyHeader, udemyLogoButtonElementSelect);

            searchBar = SeleniumUtilities.ReturnElement(udemyHeader, searchBarElementSelect);

            searchButton = SeleniumUtilities.ReturnElement(udemyHeader, searchButtonElementSelect);
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
