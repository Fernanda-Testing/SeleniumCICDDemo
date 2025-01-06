using PruebasSeleniumFernanda.Pages;

namespace PruebasSeleniumFernanda.Tests;
public class Tests_
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        //Create a new instance of Selenium Web Driver
        IWebDriver driver = new ChromeDriver();
        // Navigate to the url
        driver.Navigate().GoToUrl("https://www.google.com/");
        // Maximize de browser window
        driver.Manage().Window.Maximize();
        // Find the element
        IWebElement webElement = driver.FindElement(By.Name("q"));
        // Type in the element
        webElement.SendKeys("Flores rojas");
        // Click on the element
        webElement.SendKeys(Keys.Return);

        //webElement.Click();
    }
    [Test]
    public void EAWebSiteTest()
    {
        // 1. Create a new instance of Selenium Web Driver
        var driver = new ChromeDriver();
        // 2. Navigate to the URL
        driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        // 3. Find the Login link
        var loginLink = driver.FindElement(By.Id("loginLink"));
        // 4. Click the Login link
        loginLink.Click();

        //Explicit Wait

        WebDriverWait driverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(10))

        {
            PollingInterval = TimeSpan.FromMilliseconds(200),
            Message = "Text box UserName does not appear during that timeframe"
        };

        driverWait.IgnoreExceptionTypes(typeof(NoSuchElementException));

        var txtUserName = driverWait.Until(_ =>
        {
            var element = driver.FindElement(By.Name("UserName"));
            if ((element != null && element.Displayed))
            {
                return (IWebElement?)element;
            }
            else
            {
                return null;
            }
        });

        // 6. Typing on the textUserName
        txtUserName.SendKeys("admin");
        // 7. Find the Password text box
        var txtPassword = driver.FindElement(By.Id("Password"));
        // 8. Typing on the textUserName
        txtPassword.SendKeys("password");
        // 9. Identify the Login Button using Class Name
        //IWebElement btnLogin = driver.FindElement(By.ClassName("btn"));
        // 9. Identify the Login Button using CssSelector
        var btnLogin = driver.FindElement(By.CssSelector(".btn"));
        // 10. Click login button
        btnLogin.Submit();
    }

    //public void TestingEAWebsiteReduceCodeSize()
    //{
    //    // Create a new instance of Selenium Web Driver
    //    IWebDriver driver = new ChromeDriver();
    //    // Navigate to the url
    //    driver.Navigate().GoToUrl("http://eaapp.somee.com/");
    //    // Maximize de browser window
    //    driver.Manage().Window.Maximize();
    //    // Find and click the login link
    //    driver.FindElement(By.LinkText("Login")).Click();
    //    // Find the user name text box
    //    IWebElement userName = driver.FindElement(By.Id("UserName"));
    //    // Typing on the text userName
    //    userName.SendKeys("admin");
    //    // Find the password text box and tyiping the password
    //    driver.FindElement(By.Name("Password")).SendKeys("password");
    //    // Identifying the loggin button (using className it's unnecessary to use "." before the name)
    //    //IWebElement btnLogin = driver.FindElement(By.ClassName("btn"));
    //    // Identifying the loggin button using ccs selector and submitting.
    //    driver.FindElement(By.CssSelector(".btn")).Submit();
    //}


    //Código con método de aceptar cookies de terceros siempre que aparezcan
    //public void WorkingWithAdvancedControls()
    //{
    //    // Create a new instance of Selenium Web Driver
    //    IWebDriver driver = new ChromeDriver();
    //    try
    //    {
    //        // Navigate to the url
    //        driver.Navigate().GoToUrl("http://eaapp.somee.com/");
    //        // Maximize the browser window
    //        driver.Manage().Window.Maximize();

    //        // Aceptar el popup de cookies si aparece al cargar la página inicial (usando el método creado para ello)
    //        HandleCookiesPopup(driver);

    //        // Find and click the login link
    //        driver.FindElement(By.LinkText("Login")).Click();

    //        // Aceptar el popup de cookies si aparece después de iniciar sesión (usando el método creado para ello)
    //        HandleCookiesPopup(driver);

    //        // Find the user name text box
    //        IWebElement userName = driver.FindElement(By.Id("UserName"));
    //        // Typing on the text userName
    //        userName.SendKeys("admin");

    //        // Find the password text box and typing the password
    //        driver.FindElement(By.Name("Password")).SendKeys("password");

    //        // Identifying the login button using CSS selector and submitting
    //        driver.FindElement(By.CssSelector(".btn")).Submit();

    //        // Aceptar el popup de cookies si aparece después de iniciar sesión (usando el método creado para ello)
    //        HandleCookiesPopup(driver);

    //        // Find and click on "Employee List"
    //        driver.FindElement(By.LinkText("Employee List")).Click();

    //        // Aceptar el popup de cookies si aparece después de navegar a la lista de empleados (usando el método creado para ello)
    //        HandleCookiesPopup(driver);

    //        // Find and click on "Create new"
    //        driver.FindElement(By.LinkText("Create New")).Click();
    //    }
    //    catch (Exception ex)
    //    {
    //        // Manejar cualquier excepción inesperada durante la ejecución de la prueba
    //        Console.WriteLine($"Error: {ex.Message}");
    //    }
    //    //finally
    //    //{
    //    //    // Cerrar el navegador al final de la prueba
    //    //    driver.Quit();
    //    //}
    //}

    //// Método para manejar el popup de cookies
    //public void HandleCookiesPopup(IWebDriver driver)
    //{
    //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

    //    bool isCookiePopupPresent = true;

    //    // Crear un bucle que verifique repetidamente la aparición del popup de cookies
    //    while (isCookiePopupPresent)
    //    {
    //        try
    //        {
    //            // Localizar y hacer clic en el botón de aceptar cookies (modificar el selector según tu caso)
    //            IWebElement acceptCookiesButton = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("accept-btn")));
    //            acceptCookiesButton.Click();
    //            Console.WriteLine("Popup de cookies aceptado.");
    //        }
    //        catch (WebDriverTimeoutException)
    //        {
    //            // Si no aparece el popup de cookies, salir del bucle
    //            Console.WriteLine("El popup de cookies no apareció.");
    //            isCookiePopupPresent = false;
    //        }
    //    }
    //}

    //Código teniendo en cuenta configurar en Chrome el aceptar las cookies de terceros (Quitar cuando se termine de probar)
    //public void WorkingWithAdvancedControls()
    //{
    //    // Create a new instance of Selenium Web Driver
    //    var driver = new ChromeDriver();
    //    try
    //    {
    //        // Navigate to the url
    //        driver.Navigate().GoToUrl("http://eaapp.somee.com/");
    //        // Maximize the browser window
    //        driver.Manage().Window.Maximize();

    //        // Find and click the login link
    //        SeleniumCustomMethods.Click(driver, By.Id("loginLink"));

    //        // Find the user name text box and typing the name
    //        SeleniumCustomMethods.EnterText(driver, By.Name("UserName"), "admin");

    //        // Find the password text box and typing the password
    //        SeleniumCustomMethods.EnterText(driver, By.Name("Password"), "password");

    //        // Identifying the login button using CSS selector and submitting
    //        driver.FindElement(By.CssSelector(".btn")).Submit();

    //        // Find and click on "Employee List"
    //        SeleniumCustomMethods.Click(driver, By.LinkText("Employee List"));

    //        // Find and click on "Create new"
    //        SeleniumCustomMethods.Click(driver, By.LinkText("Create New"));

    //        // Find selector
    //        SeleniumCustomMethods.SelectDropdownByText(driver, By.Id("Grade"), "Senior");

    //    }
    //    catch (Exception ex)
    //    {
    //        // Manejar cualquier excepción inesperada durante la ejecución de la prueba
    //        Console.WriteLine($"Error: {ex.Message}");
    //    }
    //finally
    //{
    //    // Cerrar el navegador al final de la prueba
    //    driver.Quit();
    //}
    //}

    [Test]
    public void TestWithPOM()
    {
        // Create a new instance of Selenium Web Driver
        var driver = new ChromeDriver();
        // Navigate to the url
        driver.Navigate().GoToUrl("http://eaapp.somee.com/");
        // Maximize the browser window
        driver.Manage().Window.Maximize();
        // POM Initialization
        LoginPage loginPage = new LoginPage(driver);
        loginPage.ClickLogin();
        loginPage.Login("admin", "password");
    }

    [Test]

    public void testTwo()
    { }
    [Test]

    public void testThree()
    { }
}