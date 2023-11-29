using OpenQA.Selenium;
using Sysmap_udemy_test.Utilities;
using System;

namespace Sysmap_udemy_test.PageModels
{
    internal class AluraMainPageModel
    {
        private By aluraHeaderElementSelect = By.CssSelector(".header__principal");
        private IWebElement aluraHeader;

        private By searchBarElementSelect = By.Id("header-barraBusca-form-campoBusca");
        private IWebElement searchBar;

        private By searchButtonElementSelect = By.ClassName("header__nav--busca-submit");
        private IWebElement searchButton;

        public AluraMainPageModel(IWebDriver webDriver)
        {
            // init elements in here
            aluraHeader = SeleniumUtilities.ReturnElement(webDriver, aluraHeaderElementSelect);

            if (aluraHeader == null)
            {
                throw new Exception("Alura main page was not loaded properly");
            }

            searchBar = SeleniumUtilities.ReturnElement(aluraHeader, searchBarElementSelect);

            searchButton = SeleniumUtilities.ReturnElement(aluraHeader, searchButtonElementSelect);
        }

        public void SearchByKeyword(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                throw new ArgumentNullException("keyword");
            }

            if (searchBar == null)
            {
                throw new Exception("Alura main page was not loaded properly");
            }

            searchBar.SendKeys(keyword);

            searchButton.Click();
        }
    }
}
