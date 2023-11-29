using System;
using System.Windows.Forms;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Sysmap_udemy_test.PageModels;
using System.Collections.Generic;
using Sysmap_udemy_test.DatabaseModule;

namespace Sysmap_udemy_test
{
    public partial class KeywordSearchForm : Form
    {
        public KeywordSearchForm()
        {
            this.InitializeComponent();
        }

        private void searchKeywordButton_Click(object sender, EventArgs e)
        {
            string searchKeyword = keywordTextBox.Text;

            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://www.alura.com.br/");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            AluraMainPageModel aluraMainPage = new AluraMainPageModel(driver);
            aluraMainPage.SearchByKeyword(searchKeyword);

            AluraCourseListingPageModel aluraCourseListingPage = new AluraCourseListingPageModel(driver);

            List<CourseModel> courseModels = aluraCourseListingPage.ReadAllCourseInfo(driver);

            driver.Dispose();

            string databasePath = ExcelDatabase.CreateNewDatabaseDocument(searchKeyword);

            ExcelDatabase.WriteResultsIntoDatabase(databasePath, courseModels);

            MessageBox.Show("Finalized");
        }
    }
}
