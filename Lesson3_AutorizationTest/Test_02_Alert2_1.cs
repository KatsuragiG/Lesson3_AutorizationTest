using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lesson3_AutorizationTest
{
    [TestClass]
    public class Test_02_Alert2_1
    {        
        private readonly By pageAlerts = By.XPath("//div[@class = 'row']/following-sibling::div");
        private readonly By jsAllertButton = By.XPath("//button[text() = 'Click for JS Alert']");
        private readonly By jsConfirmButton = By.XPath("//button[text() = 'Click for JS Confirm']");
        private readonly By jsPromptButton = By.XPath("//button[text() = 'Click for JS Prompt']");
        private readonly By resultSection = By.Id("result");

        private readonly string expectedTextAlert = "I am a JS Alert";
        private readonly string expectedTextConfirm = "I am a JS Confirm";
        private readonly string expectedTextPrompt = "I am a JS prompt";

        private readonly string expectedResultAlert = "You successfully clicked an alert";
        private readonly string expectedResultConfirm = "You clicked: Ok";
        private readonly string expectedResultPrompt = "You entered: {0}";

        IWebDriver driver;

        [TestInitialize]
        public void TestInitialize()
        {            
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/javascript_alerts");
            WaitElements(pageAlerts);
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(MainPageIsShown(), "Page is not opened");            
            var jsAllert = driver.FindElement(jsAllertButton);            
            var random = RandomString(10);
            WaitElements(jsAllertButton);
            jsAllert.Click();

            var alert_win = driver.SwitchTo().Alert();
            Assert.AreEqual(expectedTextAlert, alert_win.Text, $"Alert doesn't contain {expectedTextAlert}");                       
            alert_win.Accept();

            var results = driver.FindElement(resultSection).Text.ToString();
            Assert.AreEqual(expectedResultAlert, results, $"Results don't contains {expectedResultAlert}");

            WaitElements(jsConfirmButton);
            var jsConfirm = driver.FindElement(jsConfirmButton);            
            jsConfirm.Click();
            Assert.AreEqual(expectedTextConfirm, alert_win.Text, $"Alert doesn't contain {expectedTextAlert}");            
            alert_win.Accept();

            results = driver.FindElement(resultSection).Text.ToString();
            Assert.AreEqual(expectedResultConfirm, results, $"Results don't contains {expectedResultConfirm}");

            WaitElements(jsPromptButton);
            var jsPrompt = driver.FindElement(jsPromptButton);            
            jsPrompt.Click();
            driver.SwitchTo().Alert();
            Assert.AreEqual(expectedTextPrompt, alert_win.Text, $"Alert doesn't contain {expectedTextPrompt}");
            alert_win.SendKeys(random);
            Thread.Sleep(500);
            alert_win.Accept();
            var expectedResultForPrompt = string.Format(expectedResultPrompt, random);

            results = driver.FindElement(resultSection).Text.ToString();
            Assert.AreEqual(expectedResultForPrompt, results, $"Results don't contains {expectedResultForPrompt}");
        }

        private void WaitElements(By itemlocator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(10000));
            wait.Until(e => e.FindElements(itemlocator));
        }

        private bool MainPageIsShown()
        {
           var shown = driver.FindElements(pageAlerts).Any();
           return shown;
        }

        private static string RandomString(int Length)
        {            
            var rnd = new Random();
            var sb = new StringBuilder(Length - 1);
            var Alphabet = "ASDaSDASDHASDasdjkkjlksjpfodkbslnahnambergv";
            int Position = 0;
            for (int i = 0; i < Length; i++)
            {                
                Position = rnd.Next(0, Alphabet.Length - 1);                
                sb.Append(Alphabet[Position]);
            }
            return sb.ToString();
        }        

        public void Exit()
        {
            driver.Quit();        
        }

    }
}
