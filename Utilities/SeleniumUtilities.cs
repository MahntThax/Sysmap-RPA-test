using OpenQA.Selenium;

namespace Sysmap_udemy_test.Utilities
{
    /// <summary>
    /// Class composed of methods to handle control of selenium actions
    /// </summary>
    internal static class SeleniumUtilities
    {
        public static bool DetectElementIsValid(IWebDriver driver, By elementSearchBy)
        {
            try
            {
                driver.FindElement(elementSearchBy);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static IWebElement ReturnElement(IWebDriver driver, By elementSearchBy)
        {
            try
            {
                return driver.FindElement(elementSearchBy);
            }
            catch
            {
                return null;
            }
        }
    }
}
