using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Lesson3_AutorizationTest
{
    [TestClass]
    public class Test_04_Actions_Slider
    {
        private readonly By page = By.XPath("//div[@class = 'row']/following-sibling::div");
        private readonly By slider = By.XPath("//div[@class = 'sliderContainer']//input");
        private readonly By sliderValue = By.XPath("//div[@class = 'sliderContainer']//span");

        IWebDriver driver;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/horizontal_slider");
            Thread.Sleep(5000);
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(mainPageIsShown(), "Page is not opened");

            var rundomsValue = randomValue();
            slideToRight(rundomsValue);
            var sliderVal = driver.FindElement(sliderValue).Text.ToString();
            double expectedValue = (double)rundomsValue / 2;
            Assert.AreEqual(expectedValue.ToString(), sliderVal, $"{expectedValue} are not equals to {sliderVal}");
        }

        private int randomValue()
        {
            Random rnd = new Random();
            var value = rnd.Next(1, 9);
            return value;
        }

        private void slideToRight(int value)
        {
            for (int i = 0; i < value; i++)
            {
                driver.FindElement(slider).SendKeys(Keys.ArrowRight);
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
