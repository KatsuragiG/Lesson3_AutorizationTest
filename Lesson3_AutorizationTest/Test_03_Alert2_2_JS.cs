using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Text;
using System.Threading;

namespace Lesson3_AutorizationTest
{
    [TestClass]
    public class Test_03_Alert2_2_JS
    {        
        private readonly By pageAlerts = By.XPath("//div[@class = 'row']/following-sibling::div");        
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
            WaitPageToLoad(pageAlerts);
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(MainPageIsShown(), "Page is not opened");
                      
            var random = RandomString(10);
            Thread.Sleep(500);
            ((IJavaScriptExecutor)driver).ExecuteScript("jsAlert()");            

            var alert_win = driver.SwitchTo().Alert();
            Assert.AreEqual(expectedTextAlert, alert_win.Text, $"Alert doesn't contain {expectedTextAlert}");
            Thread.Sleep(500);            
            alert_win.Accept();

            var results = driver.FindElement(resultSection).Text.ToString();
            Assert.AreEqual(expectedResultAlert, results, $"Results don't contains {expectedResultAlert}");
            
            Thread.Sleep(500);
            ((IJavaScriptExecutor)driver).ExecuteScript("jsConfirm()");
            Assert.AreEqual(expectedTextConfirm, alert_win.Text, $"Alert doesn't contain {expectedTextAlert}");
            Thread.Sleep(500);
            alert_win.Accept();

            results = driver.FindElement(resultSection).Text.ToString();
            Assert.AreEqual(expectedResultConfirm, results, $"Results don't contains {expectedResultConfirm}");
            
            Thread.Sleep(500);
            ((IJavaScriptExecutor)driver).ExecuteScript("jsPrompt()");
            driver.SwitchTo().Alert();
            Assert.AreEqual(expectedTextPrompt, alert_win.Text, $"Alert doesn't contain {expectedTextPrompt}");
            alert_win.SendKeys(random);
            Thread.Sleep(500);
            alert_win.Accept();
            var expectedResultForPrompt = string.Format(expectedResultPrompt, random);

            results = driver.FindElement(resultSection).Text.ToString();
            Assert.AreEqual(expectedResultForPrompt, results, $"Results don't contains {expectedResultForPrompt}");
        }

        private bool MainPageIsShown()
        {
           var shown = driver.FindElement(pageAlerts).Displayed;
           return shown;
        }

        private string RandomString(int Length)
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

        private void WaitPageToLoad(By itemlocator)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(10000));
            wait.Until(e => e.FindElements(itemlocator));
        }

        [TestCleanup]
        public void Exit()
        {
            driver.Quit();        
        }

    }
}
