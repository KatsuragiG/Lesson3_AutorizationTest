using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace Lesson3_AutorizationTest
{
    [TestClass]
    public class Test_06_Hovers
    {        
        private readonly By page = By.XPath("//div[@class = 'row']/following-sibling::div");
        private const string usersIcon = "//div[@class = 'figure'][{0}]";
        private const string userName = usersIcon + "//h5";
        private const string userLink = usersIcon + "//a";
        
        private string expectedUserName = "name: user{0}";        

        IWebDriver driver;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/hovers");
            Thread.Sleep(3000);
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(mainPageIsShown(), "Page is not opened");            

            for (int i = 1; i <= 3; i++)
            {
                Actions action = new Actions(driver);
                action.MoveToElement(driver.FindElement(By.XPath(string.Format(usersIcon, i)))).Perform();
                Thread.Sleep(500);
                var userNames = driver.FindElement(By.XPath(string.Format(userName, i))).Text;
                Assert.AreEqual(string.Format(expectedUserName, i), userNames, $"User name {string.Format(expectedUserName, i)} are not equal {userNames}");
                var userLinks = driver.FindElement(By.XPath(string.Format(userLink, i)));
                userLinks.Click();
                Thread.Sleep(500);
                var expectedUrl = driver.Url.Contains($"/users/{i}");
                Assert.IsTrue(expectedUrl, $"Url are not contains user id");
                driver.Navigate().Back();
                Assert.IsTrue(mainPageIsShown(), "Page is not opened");
                Thread.Sleep(500);
            }
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
