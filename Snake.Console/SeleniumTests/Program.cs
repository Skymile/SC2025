
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

var driver = new ChromeDriver();

driver.Navigate().GoToUrl("https://www.w3schools.com/cs/index.php");

Thread.Sleep(1_000);

var menu = driver
    .FindElement(By.Id("leftmenuinnerinner"));

var links = menu
    .FindElements(By.TagName("a"))
    .Select(e => (Text: e.GetAttribute("innerHTML"), Link: e.GetAttribute("href")))
    .ToArray();

foreach (var i in links)
    Console.WriteLine($"{i.Text}: {i.Link}");

foreach (var i in links)
{
    driver.Navigate().GoToUrl(i.Link);
    Thread.Sleep(500);
}
