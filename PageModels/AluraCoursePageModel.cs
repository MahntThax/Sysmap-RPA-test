using OpenQA.Selenium;
using Sysmap_udemy_test.DatabaseModule;
using System.Collections.Generic;

namespace Sysmap_udemy_test.PageModels
{
    internal class AluraCoursePageModel
    {
        private By titleSelector = By.ClassName("formacao-headline-titulo");
        private IWebElement titleElement;

        private By descriptionSelector = By.ClassName("formacao-headline-subtitulo");
        private IWebElement descriptionElement;

        private By totalHoursSelector = By.ClassName("formacao__info-conclusao");
        private IWebElement totalHoursElement;

        private By totalHoursTextSelector = By.ClassName("formacao__info-destaque");
        private IWebElement totalHoursTextElement;

        private By instructorContainerSelector = By.Id("instrutores");
        private IWebElement instructorContainerElement;

        private By instructorListSelector = By.ClassName("formacao-instrutores-lista");
        private IWebElement instructorListElement;

        private By instructorInfoContainerSelector = By.ClassName("formacao-instrutores-instrutor");

        public AluraCoursePageModel(IWebDriver driver)
        {
            titleElement = driver.FindElement(titleSelector);
            descriptionElement = driver.FindElement(descriptionSelector);
            totalHoursElement = driver.FindElement(totalHoursSelector);
            totalHoursTextElement = totalHoursElement.FindElement(totalHoursTextSelector);
            instructorContainerElement = driver.FindElement(instructorContainerSelector);
            instructorListElement = instructorContainerElement.FindElement(instructorListSelector);
        }

        public CourseModel ReadCourseDetails()
        {
            string courseInstructors = string.Empty;

            IEnumerable<IWebElement> instructorsList = instructorListElement.FindElements(instructorInfoContainerSelector);

            foreach (IWebElement instructorContainer in instructorsList)
            {
                IWebElement instructorName = instructorContainer.FindElement(By.ClassName("formacao-instrutor-nome"));
                courseInstructors += instructorName.Text;
            }

            return new CourseModel(
                titleElement.Text,
                descriptionElement.Text,
                courseInstructors,
                totalHoursTextElement.Text);
        }
    }
}
