using OpenQA.Selenium;
using Sysmap_udemy_test.DatabaseModule;
using Sysmap_udemy_test.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sysmap_udemy_test.PageModels
{
    internal class UdemyCourseListingPageModel
    {
        private By pageMainContentSelector = By.ClassName("ud-main-content");
        private IWebElement pageMainContent;

        private By pageCourseDirectoryContainerSelector = By.ClassName("course-directory--container");
        private IWebElement pageCourseDirectory;

        private By pageFilterPanelCourseListSelector = By.ClassName("filter-panel--paginated-course-list");
        private IWebElement pageFilterPanelCourseList;

        private By pageCourseListSelector = By.ClassName("course-list--container");
        private IWebElement pageCourseList;

        private By pageCourseInfoSelector = By.ClassName("popper-module--popper");
        private List<IWebElement> pageCourseInfoList;

        /// <summary>
        /// Initiate the necessary elements to process the list of courses
        /// </summary>
        /// <param name="webDriver">The google chrome driver</param>
        /// <exception cref="Exception">Throw a exception in case it can't load properly the elements</exception>
        public UdemyCourseListingPageModel(IWebDriver webDriver)
        {
            pageMainContent = SeleniumUtilities.ReturnElement(webDriver, pageMainContentSelector);

            if (pageMainContent == null)
            {
                throw new Exception("Udemy courses listing page was not loaded properly");
            }

            pageCourseDirectory = SeleniumUtilities.ReturnElement(pageMainContent, pageCourseDirectoryContainerSelector);

            pageFilterPanelCourseList = SeleniumUtilities.ReturnElement(pageCourseDirectory, pageFilterPanelCourseListSelector);

            pageCourseList = SeleniumUtilities.ReturnElement(pageFilterPanelCourseList, pageCourseListSelector);

            pageCourseInfoList = pageCourseList.FindElements(pageCourseInfoSelector).ToList();
        }

        /// <summary>
        /// Read all of the courses listed in the page
        /// </summary>
        /// <returns></returns>
        public List<CourseModel> ReadAllCourseInfo()
        {
            List<CourseModel> result = new List<CourseModel>();

            foreach (var courseInfo in pageCourseInfoList)
            {
                if (courseInfo != null)
                {
                    result.Add(ReadCourseInfo(courseInfo));
                }
            }

            return result;
        }

        /// <summary>
        /// Read the information of one course
        /// </summary>
        /// <param name="courseInfo">the course to read the information</param>
        /// <returns>the object containing the course information</returns>
        private CourseModel ReadCourseInfo(IWebElement courseInfo)
        {
            IWebElement courseMainContent = courseInfo.FindElement(By.ClassName("course-card-module--main-content"));

            IWebElement titleContent = courseMainContent.FindElement(By.ClassName("course-card-title-module--title"));
            IWebElement instructorContent = courseMainContent.FindElement(By.ClassName("course-card-instructors-module--instructor-list"));
            IWebElement courseHeadline = courseMainContent.FindElement(By.ClassName("ud-text-sm course-card-module--course-headline");
            IWebElement courseDetailContent = courseMainContent.FindElement(By.ClassName("course-card-details-module"));
            IWebElement courseTotalHoursContent = courseDetailContent.FindElement(By.CssSelector("#u5-popper-trigger--303 > div:nth-child(2) > div:nth-child(5) > div:nth-child(1) > span:nth-child(1)"));

            return new CourseModel(
                titleContent.Text,
                courseHeadline.Text,
                instructorContent.Text,
                courseTotalHoursContent.Text
            );
        }
    }
}
