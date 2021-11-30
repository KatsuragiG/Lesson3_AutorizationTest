using OpenQA.Selenium;
using System;

namespace BaseTest
{
    public class Browser
    {
    }

    public void StartDriver()
    {
        IWebDriver driver;
        driver = new OpenQA.Selenium.Chrome.ChromeDriver();
        driver.Manage().Window.Maximize();
    }
}
