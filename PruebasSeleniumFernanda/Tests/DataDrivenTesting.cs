using FluentAssertions;
using PruebasSeleniumFernanda.Models;
using PruebasSeleniumFernanda.Pages;
using System.Text.Json;


namespace PruebasSeleniumFernanda.Tests.Tests;
public class DataDrivenTesting
{
    private IWebDriver driver;

    [SetUp]
    public void SetUp()
    {
        driver = new ChromeDriver();
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        driver.Manage().Window.Maximize();
    }

    [Test]
    [Category("ddt")]
    [TestCaseSource(nameof(LoginJsonDataSource))]
    public void TestWithPOM(LoginModel loginModel)
    {
        // Arrange - Inicialización de Page Object Model
        LoginPage loginPage = new LoginPage(driver);

        // Act
        loginPage.ClickLogin();
        loginPage.Login(loginModel.UserName, loginModel.Password);

        // Assert
        var getLoggedIn = loginPage.IsLoggedIn();

        getLoggedIn.employeeDetails.Should().BeTrue();
        getLoggedIn.manageUsers.Should().BeTrue();

        //Assert.IsTrue(getLoggedIn); deprecado en versiones anteriores 
        Assert.That(getLoggedIn.employeeDetails && getLoggedIn.manageUsers, Is.True);
    }

    [Test]
    [Category("ddt")]
    public void TestWithPOMWithJsonData()
    {
        string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login.json");
        var jsonString = File.ReadAllText(jsonFilePath);

        // Deserializar como una lista de LoginModel
        var loginModels = JsonSerializer.Deserialize<List<LoginModel>>(jsonString);

        // Usa el primer objeto de la lista para esta prueba
        var loginModel = loginModels?.FirstOrDefault();
        if (loginModel == null)
        {
            Assert.Fail("No se encontraron datos de login en el archivo JSON.");
            return;
        }

        // Inicialización de Page Object Model
        LoginPage loginPage = new LoginPage(driver);
        loginPage.ClickLogin();
        loginPage.Login(loginModel.UserName, loginModel.Password);
    }

    public static IEnumerable<LoginModel> Login()
    {
        yield return new LoginModel()
        {
            UserName = "admin",
            Password = "password"
        };
    }

    public static IEnumerable<LoginModel> LoginJsonDataSource()
    {
        string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login.json");
        var jsonString = File.ReadAllText(jsonFilePath);

        var loginModel = JsonSerializer.Deserialize<List<LoginModel>>(jsonString);

        foreach (var loginData in loginModel)
        {
            yield return loginData;
        }
    }

    private void ReadJsonFile()
    {
        string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Login.json");
        var jsonString = File.ReadAllText(jsonFilePath);
        var loginModel = JsonSerializer.Deserialize<LoginModel>(jsonString);

        Console.WriteLine($"UserName: {loginModel.UserName} Password: {loginModel.Password}");
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}