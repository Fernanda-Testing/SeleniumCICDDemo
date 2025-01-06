using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using PruebasSeleniumFernanda.Driver;
using PruebasSeleniumFernanda.Pages;

namespace PruebasSeleniumFernanda.Tests;

[TestFixture("admin", "password", DriverType.Firefox)]
public class SeleniumGridTests
{
    private IWebDriver _driver;
    private readonly string username;
    private readonly string password;
    private readonly DriverType driverType;

    public SeleniumGridTests(string username, string password, DriverType driverType)
    {
        this.username = username;
        this.password = password;
        this.driverType = driverType;
    }

    [SetUp]
    public void SetUp()
    {
        _driver = GetDriverType(driverType);
        _driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        _driver.Manage().Window.Maximize();
    }
    private IWebDriver GetDriverType(DriverType driverType)
    {
        return driverType switch
        {
            DriverType.Chrome => new RemoteWebDriver(new Uri("http://localhost:4444"), new ChromeOptions()),
            DriverType.Firefox => new RemoteWebDriver(new Uri("http://localhost:4444"), new FirefoxOptions()),
            DriverType.Edge => new RemoteWebDriver(new Uri("http://localhost:4444"), new EdgeOptions()),
            _ => throw new ArgumentException("Unsupported browser type")
        };
    }

    [Test]
    [TestCase("Chrome", "25")]

    public void TestBrowserVersion(string browser, string version)
    {
        Console.WriteLine($"The browser is {browser} with version {version}");
    }

    [Test]
    [Category("smoke")]
    public void TestWithPOM()
    {
        LoginPage loginPage = new LoginPage(_driver);
        loginPage.ClickLogin();
        loginPage.Login(username, password);
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Dispose();
        //Llamar a Dispose() asegura que todos los recursos asociados con el driver se liberen correctamente.
        //El operador de navegación segura ?. garantiza que no se lanzará una excepción si driver es null por alguna razón.
    }
}