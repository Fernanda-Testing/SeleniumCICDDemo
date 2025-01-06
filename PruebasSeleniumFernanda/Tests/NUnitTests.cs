using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using PruebasSeleniumFernanda.Driver;
using PruebasSeleniumFernanda.Pages;

namespace PruebasSeleniumFernanda.Tests.Tests;

[TestFixture("admin", "password", DriverType.Chrome)]
public class NUnitTests
{
    private IWebDriver _driver;
    private readonly string username;
    private readonly string password;
    private readonly DriverType driverType;
    private ExtentReports _extentReports;
    private ExtentTest _extentTests;
    private ExtentTest _testNode;

    public NUnitTests(string username, string password, DriverType driverType)
    {
        this.username = username;
        this.password = password;
        this.driverType = driverType;
    }

    [SetUp]
    public void SetUp()
    {
        SetupExtentReports();
        _driver = GetDriverType(driverType);
        _testNode = _extentTests.CreateNode("Setup and Tear Down").Pass("Browser Launched");
        _driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        _driver.Manage().Window.Maximize();
    }

    private IWebDriver GetDriverType(DriverType driverType)
    {
        return driverType switch
        {
            DriverType.Chrome => new OpenQA.Selenium.Chrome.ChromeDriver(),
            DriverType.Firefox => new OpenQA.Selenium.Firefox.FirefoxDriver(),
            DriverType.Edge => new OpenQA.Selenium.Edge.EdgeDriver(),
            _ => throw new ArgumentException("Invalid driver type")
        };
    }
    private void SetupExtentReports()
    {
        // Inicialización del reporte
        _extentReports = new ExtentReports();
        var spark = new ExtentSparkReporter("TestReport.html");
        _extentReports.AttachReporter(spark);
        _extentReports.AddSystemInfo("OS", "Windows 11");
        _extentReports.AddSystemInfo("Browser", driverType.ToString());
        _extentTests = _extentReports.CreateTest("Login test with POM").Log(Status.Info, "Extent report initialized");
    }

    [Test]
    [TestCaseSource(nameof(BrowserTestCases))]
    public void TestBrowserVersion(string browser, string version)
    {
        Console.WriteLine($"The browser is {browser} with version {version}");
    }

    private static IEnumerable<TestCaseData> BrowserTestCases => new[]
    {
    new TestCaseData("Chrome", "25"),
    new TestCaseData("Firefox", "92"),
};

    [Test]
    [Category("smoke")]
    public void TestWithPOM()
    {
        // Prueba Test with GitHub
        // POM initalization
        LoginPage loginPage = new LoginPage(_driver);

        loginPage.ClickLogin();
        _extentTests.Log(Status.Pass, "Click login");

        loginPage.Login(username, password);
        _extentTests.Log(Status.Pass, "UserName and Password entered with login happend");

        var getLoggedIn = loginPage.IsLoggedIn();
        Assert.That(getLoggedIn.employeeDetails && getLoggedIn.manageUsers, Is.True);
        _extentTests.Log(Status.Pass, "Assertion successful");
    }

    [TearDown]
    public void TearDown()
    {
        _driver?.Dispose();
        //Llamar a Dispose() asegura que todos los recursos asociados con el driver se liberen correctamente.
        //El operador de navegación segura ?. garantiza que no se lanzará una excepción si driver es null por alguna razón.
        _testNode.Pass("Browser Quit");
        // Guardar el reporte
        _extentReports.Flush();
    }
}