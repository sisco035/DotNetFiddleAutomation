using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Keys = OpenQA.Selenium.Keys;

namespace DotNetFiddleAutomation.POM
{
    public class FiddleHomePageModel
    {
        private IWebDriver driver {get;set;}

        public FiddleHomePageModel(IWebDriver _driver)
        {
            driver = _driver;
        }

        public void RunConsole()
        {
            driver.FindElement(By.Id("run-button")).Click();           

            WebDriverWait wait = new WebDriverWait(driver,
                System.TimeSpan.FromSeconds(10));
        }

        public string GetConsoleOutput()
        {
            IWebElement element = driver.FindElement(By.Id("output"));

            return element.Text;
        }

        public void ShareFiddle()
        {
            driver.FindElement(By.Id("Share")).Click();

            WebDriverWait wait = new WebDriverWait(driver,
                System.TimeSpan.FromSeconds(15));
            wait.Until(driver =>
            driver.FindElement(By.Id("ShareLink")));
        }

        public string GetShareLink()
        {     
            driver.FindElement(By.XPath("//a[@class='copy-clipboard']")).Click();

            IWebElement fiddleNameTextbox = driver.FindElement(By.XPath("//div[@class='name-container']/input"));
            fiddleNameTextbox.SendKeys(Keys.Control + "v");
            fiddleNameTextbox.SendKeys(Keys.Tab);

            WebDriverWait wait = new WebDriverWait(driver,
              System.TimeSpan.FromSeconds(15));
                wait.Until(driver =>
                     driver.FindElement(By.Id("fiddle-name")).GetAttribute("value") !="");

            IWebElement element =
                 driver.FindElement(By.Id("fiddle-name"));            
           
            return element.GetAttribute("value");
        }
    }
}
