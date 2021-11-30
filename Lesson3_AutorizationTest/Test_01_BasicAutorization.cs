using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace Lesson3_AutorizationTest
{
    [TestClass]
    public class Test_01_BasicAutorization
    {
        private readonly By successedText = By.XPath("//div[@class = 'example']//p");
        private readonly string password = "admin";
        private readonly string userName = "admin";
        private readonly string urlPage = "http://{0}:{0}@the-internet.herokuapp.com/basic_auth";
        private readonly string expectedText = "Congratulations! You must have the proper credentials.";       

        IWebDriver driver;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TestMethod]
        public void TestMethod1()
        {            
            var urlConnected = string.Format(urlPage, password, userName);
            driver.Navigate().GoToUrl(urlConnected);
            var actualText = driver.FindElement(successedText).Text.ToString();
            Assert.AreEqual(expectedText, actualText, "successedText are not present");
        }

        [TestCleanup]
        public void Exit()
        {
            driver.Quit();
        }
    }
}
