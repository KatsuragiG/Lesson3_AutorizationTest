using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace Lesson3_AutorizationTest
{
    [TestClass]
    public class Test_05_Alert3
    {
        private readonly By page = By.XPath("//div[@class = 'example']");
        private readonly By menu = By.Id("hot-spot");

        private string expectedTextAlert = "You selected a context menu";

        IWebDriver driver;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/context_menu");
            Thread.Sleep(3000);
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(mainPageIsShown(), "Page is not opened");

            var jsAllert = driver.FindElement(menu);
            Actions action = new Actions(driver);
            Thread.Sleep(500);
            action.ContextClick(jsAllert).Perform();            

            var alert_win = driver.SwitchTo().Alert();            
            Assert.AreEqual(expectedTextAlert, alert_win.Text, $"Alert doesn't contain {expectedTextAlert}");
            Thread.Sleep(500);
            alert_win.Accept();
        }
        private bool mainPageIsShown()
        {
            var shown = driver.FindElement(page).Displayed;
            return shown;
        }

        [TestCleanup]

        public void exit()
        {
            driver.Quit();
        }

    }
}
