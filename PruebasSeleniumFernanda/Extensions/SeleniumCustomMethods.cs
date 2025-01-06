namespace PruebasSeleniumFernanda.Extensions;
public static class SeleniumCustomMethods
{

    public static void ClickElement(this IWebElement locator)
    {
        locator.Click();
    }

    public static void SubmitElement(this IWebElement locator)
    {
        locator.Submit();
    }

    public static void EnterText(this IWebElement locator, string text)
    {
        locator.Clear();
        locator.SendKeys(text);

    }

    public static void SelectDropdownByText(this IWebElement locator, string text)
    {
        var selectElement = new SelectElement(locator);
        selectElement.SelectByText(text);
    }

    public static void SelectDropdownByValue(this IWebElement locator, string value)
    {
        var selectElement = new SelectElement(locator);
        selectElement.SelectByText(value);
    }
}