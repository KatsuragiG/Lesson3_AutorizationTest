using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
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

        private string expectedTextAlert = "I am a JS Alert";
        private string expectedTextConfirm = "I am a JS Confirm";
        private string expectedTextPrompt = "I am a JS prompt";

        private string expectedResultAlert = "You successfully clicked an alert";
        private string expectedResultConfirm = "You clicked: Ok";
        private string expectedResultPrompt = "You entered: {0}";

        IWebDriver driver;

        [TestInitialize]
        public void TestInitialize()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://the-internet.herokuapp.com/javascript_alerts");
            Thread.Sleep(10000);
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(mainPageIsShown(), "Page is not opened");
                      
            var random = randomString(10);
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

        private bool mainPageIsShown()
        {
           var shown = driver.FindElement(pageAlerts).Displayed;
           return shown;
        }

        private string randomString(int Length)
        {            
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(Length - 1);
            var Alphabet = "ASDaSDASDHASDasdjkkjlksjpfodkbslnahnambergv";
            int Position = 0;
            for (int i = 0; i < Length; i++)
            {                
                Position = rnd.Next(0, Alphabet.Length - 1);                
                sb.Append(Alphabet[Position]);
            }
            return sb.ToString();
        }        

        [TestCleanup]
        public void exit()
        {
            driver.Quit();        
        }

    }
}
