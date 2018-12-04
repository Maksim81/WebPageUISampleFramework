using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BetOlymp_Malta
{
    public class BetOlymp_Malta
    {
        private IWebDriver _driver;

        public bool IsElementVisible(IWebElement element)
        {
            return element.Displayed && element.Enabled;
        }

        public bool IsElementDisplayed(IWebElement element) //Unfortunately, not always Displayed == Enabled, so please use appropriate method
        {
            return element.Displayed;
        }

        public void StartFromHomePage()
        {
            _driver.Navigate().GoToUrl("http://10.10.0.118:6681/bov2/en/sports/");
        }

        public void Login()
        {
            _driver.FindElement(By.Id("login-username")).Click();
            _driver.FindElement(By.Id("login-username")).SendKeys("Tester291104");
            _driver.FindElement(By.Id("login-password")).Click();
            _driver.FindElement(By.Id("login-password")).SendKeys("Test1234");
            _driver.FindElement(By.Id("login_btn")).Click();
        }

        public void ClickOnSportsBetting()
        {
            _driver.FindElement(By.PartialLinkText("SPORTS BETTING")).Click();
        }

        public void ClickOnFootball()
        {
            _driver.FindElement(By.PartialLinkText("FOOTBALL")).Click();
        }

        public void ClickOnEnglandPremiereLeague()
        {
            _driver.FindElement(By.PartialLinkText("England. Premier League")).Click();
        }

        public void ClickOnFirstPredict()
        {
            _driver.FindElement(By.ClassName("ch_l c_bbtn")).Click();
        }

        public string GetFirstPredictDetails()
        {
            String matchDetails = _driver.FindElement(By.XPath("//*[@id='pane - target']/div[2]/div[2]/div[1]/div[1]/div[1]")).ToString();
            Debug.WriteLine(matchDetails);
            return matchDetails;
        }

        [SetUp]
        public void SetupTest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _driver = new ChromeDriver();
        }

        [Test, Description ("Logo presents and available")]
        public void HomePage()
        {
            StartFromHomePage();
            IWebElement element = _driver.FindElement(By.ClassName("head_logo_wrp")); //Here we find element by CSS selector
            Assert.True(IsElementVisible(element));
        }

        [Test, Description("Base Login")]
        public void BaseLogin()
        {
            StartFromHomePage();
            Login();
            Thread.Sleep(3000);
            IWebElement element = _driver.FindElement(By.PartialLinkText("Deposit"));
            Assert.True(IsElementVisible(element));
        }

        [Test, Description("Place simple bet")]
        public void PlaceBet()
        {
            StartFromHomePage();
            Login();
            ClickOnSportsBetting();
            ClickOnFootball();
            ClickOnEnglandPremiereLeague();
            //ClickOnFirstPredict();
            //Console.WriteLine("Console output");
            GetFirstPredictDetails();
            //Debug.WriteLine("Console output");
            //IWebElement element = _driver.FindElement(By.XPath("//*[@id="cm - 1100303313 - 1"]/span[2]"));
            //Assert.True(IsElementVisible(element));
        }



        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
                _driver.Quit();
        }
    }
}
