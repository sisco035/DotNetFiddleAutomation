using DotNetFiddleAutomation.POM;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace DotNetFiddleAutomation
{
    [TestFixture]
    public class DotNetFiddleTests
    {
        private IWebDriver driver;
        public string homeURL;
     
        [SetUp]
        public void SetupTest()
        {
            homeURL = "https://dotnetfiddle.net/";
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl(homeURL);
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver,
             TimeSpan.FromSeconds(15));
            wait.Until(driver =>
            driver.FindElement(By.Id("run-button")));     
        }    

        [TearDown]
        public void TearDownTest()
        {
            driver.Close();
        }

        [Test(Description = "Runs DotNetFiddle and asserts console output")]
        public void RunDotNetFiddleConsoleVerifyOutput()
        {
            try
            {
                FiddleHomePageModel fiddleHomePage = new FiddleHomePageModel(driver);
                fiddleHomePage.RunConsole();

                Assert.AreEqual("Hello World", fiddleHomePage.GetConsoleOutput());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }           
        }

        [Test(Description = "Runs DotNetFiddle clicks share and asserts shared URL contains the root url address")]
        public void ShareDotNetFiddleVerifySharedLinkStartsWithRootDotnetFiddleUrlAddress()
        {
            try
            {
                FiddleHomePageModel fiddleHomePage = new FiddleHomePageModel(driver);
                fiddleHomePage.ShareFiddle();

                StringAssert.StartsWith("https://dotnetfiddle.net/", fiddleHomePage.GetShareLink());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }          
        }
    }
}
