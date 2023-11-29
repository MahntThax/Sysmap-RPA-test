using OpenQA.Selenium;
using Sysmap_udemy_test.DatabaseModule;
using Sysmap_udemy_test.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sysmap_udemy_test.PageModels
{
    internal class AluraCourseListingPageModel
    {
        private By pageSearchResultsContainerSelector = By.Id("busca");
        private IWebElement pageCourseDirectory;

        private By pageCourseListSelector = By.Id("busca-resultados");
        private IWebElement pageCourseList;

        private By pageCourseResultSelector = By.ClassName("busca-resultado");

        /// <summary>
        /// Initiate the necessary elements to process the list of courses
        /// </summary>
        /// <param name="webDriver">The google chrome driver</param>
        /// <exception cref="Exception">Throw a exception in case it can't load properly the elements</exception>
        public AluraCourseListingPageModel(IWebDriver webDriver)
        {
            pageCourseDirectory = SeleniumUtilities.ReturnElement(webDriver, pageSearchResultsContainerSelector);
            if (pageCourseDirectory == null)
            {
                throw new Exception("Udemy courses listing page was not loaded properly");
            }

            pageCourseList = pageCourseDirectory.FindElement(pageCourseListSelector);
        }

        /// <summary>
        /// Read all of the courses listed in the page
        /// </summary>
        /// <returns></returns>
        public List<CourseModel> ReadAllCourseInfo(IWebDriver driver)
        {
            List<CourseModel> result = new List<CourseModel>();

            IEnumerable<IWebElement> pageCourseResultListElement = pageCourseList.FindElements(pageCourseResultSelector);

            foreach(IWebElement pageCourseResultElement in pageCourseResultListElement)
            {
                string originalWindowHandle = driver.CurrentWindowHandle;
                IWebElement resultLinkElement = pageCourseResultElement.FindElement(By.ClassName("busca-resultado-link"));
                string courseUrl = resultLinkElement.GetAttribute("href");
                driver.SwitchTo().NewWindow(WindowType.Tab);
                driver.Navigate().GoToUrl(courseUrl);
                
                try
                {
                    AluraCoursePageModel aluraCoursePageModel = new AluraCoursePageModel(driver);
                    result.Add(aluraCoursePageModel.ReadCourseDetails());
                }
                catch
                {
                    // some pages can't read because it needs to be logged in
                    // the correct way would be to handle different errors, but for now that is enough
                }

                driver.Close();
                driver.SwitchTo().Window(originalWindowHandle);
            }

            return result;
        }
    }
}
