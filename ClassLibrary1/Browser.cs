using OpenQA.Selenium;

namespace ClassLibrary1
{ 
    public class Browser
    {
        private IWebDriver driver = new OpenQA.Selenium.Chrome.ChromeDriver();       

        public void InitializeDriverAndGoToUrl(string url)
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(url);
        }
    }
}
