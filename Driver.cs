using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using System;

namespace Vivino_AndroidSoftware
{
    public static class Driver
    {
        public static AndroidDriver<AndroidElement> driver;
        public static string VivinoAppStartActivity { get; private set; }

        public static void InitializeDriver(string startActivityName)
        {
            AppiumOptions appiumOptions = new AppiumOptions();
            appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");

            appiumOptions.AddAdditionalCapability("appPackage", Helpers.VivinoAppPackage);
            appiumOptions.AddAdditionalCapability("appActivity", startActivityName);

            driver = new AndroidDriver<AndroidElement>(new Uri(Helpers.AppiumServerUrl), appiumOptions);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Helpers.WaitSecsForElements);
        }

        public static AndroidElement ScrollToElement(string element)
        {
            return driver.FindElementByAndroidUIAutomator(
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceIdMatches(" +
                "\"" + element + "\"))");
        }
    }
}
