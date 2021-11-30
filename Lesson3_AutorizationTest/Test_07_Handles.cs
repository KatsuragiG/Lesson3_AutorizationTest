using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Threading;

namespace Lesson3_AutorizationTest
{
    [TestClass]
    public class Test_07_Handles
    {
        private readonly By page = By.XPath("//div[@class = 'row']/following-sibling::div");
        private readonly By linkWindow = By.XPath("//*[@href = '/windows/new']");
        private readonly By pageName = By.XPath("//h3[text()]");

        private readonly string expectedPageName = "New Window";

        IWebDriver driver;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/windows");
            Thread.Sleep(3000);
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(mainPageIsShown(), "Page is not opened");            

            var originalWindow = driver.CurrentWindowHandle;
            var linksToClick = driver.FindElements(linkWindow);
            Assert.IsTrue(linksToClick.Any());
            linksToClick.First().Click();
            Thread.Sleep(2000);

            SwitchToLastWindow();
            var actualNameForWindow = driver.FindElement(pageName).Text;
            var titleWindow = driver.Title;
            Assert.AreEqual(expectedPageName, actualNameForWindow, $"{expectedPageName} are not equal to {actualNameForWindow}");
            Assert.AreEqual(actualNameForWindow, titleWindow, $"{actualNameForWindow} is not equal {titleWindow}");
            var firstWindow = driver.CurrentWindowHandle;

            SwitchToFirstWindow();
            Assert.AreEqual(originalWindow, driver.CurrentWindowHandle, "The main page are not opened");
            linksToClick.First().Click();
            Thread.Sleep(500);

            SwitchToLastWindow();
            var secondWindow = driver.CurrentWindowHandle;            
            actualNameForWindow = driver.FindElement(pageName).Text;
            titleWindow = driver.Title;
            Assert.AreEqual(expectedPageName, actualNameForWindow, $"{expectedPageName} are not equal to {actualNameForWindow}");
            Assert.AreEqual(actualNameForWindow, titleWindow, $"{actualNameForWindow} is not equal {titleWindow}");
            Thread.Sleep(500);

            driver.SwitchTo().Window(firstWindow);            
            driver.Close();
            Thread.Sleep(500);
            driver.SwitchTo().Window(originalWindow);
            driver.Close();
            Thread.Sleep(500);
            driver.SwitchTo().Window(secondWindow);
            driver.Close();
        }       

        private void SwitchToLastWindow()
        {
            var availableWindows = driver.WindowHandles;
            if (availableWindows.Count > 1)
            {
                driver.SwitchTo().Window(availableWindows[availableWindows.Count - 1]);
            }

        }

        private void SwitchToFirstWindow()
        {
           driver.SwitchTo().Window(driver.WindowHandles[0]);
        }

        private void WaitElements(By itemlocator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(1000));
            wait.Until(e => e.FindElements(itemlocator));
        }

        private bool mainPageIsShown()
        {
            var shown = driver.FindElement(page).Displayed;
            return shown;
        }
    }
}
