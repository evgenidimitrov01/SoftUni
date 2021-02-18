using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Vivino_AndroidSoftware.Activities
{
    public class NewVintageDetailsActivity
    {
        public AndroidDriver<AndroidElement> driver;

        public NewVintageDetailsActivity(AndroidDriver<AndroidElement> driver)
        {
            this.driver = driver;
        }

        public AndroidElement TxtRating => driver.FindElementById(Helpers.VivinoAppPackage + ":id/rating");
        public AndroidElement SummaryBox => driver.FindElementByXPath("//android.widget.TextView[@resource-id='vivino.web.app:id/header']/..");
        public AppiumWebElement FactsTab => SummaryBox.FindElementByXPath("//*[@resource-id='vivino.web.app:id/tabs']//android.widget.TextView[2]");
        public AppiumWebElement FirstFactTitle => SummaryBox.FindElementById(Helpers.VivinoAppPackage + ":id/wine_fact_title");
        public AppiumWebElement FirstFactText => SummaryBox.FindElementById(Helpers.VivinoAppPackage + ":id/wine_fact_text");
        public AndroidElement BtnAddToWishLish => driver.FindElementById(Helpers.VivinoAppPackage + ":id/wish_button");
        public AndroidElement TxtWineName => driver.FindElementById(Helpers.VivinoAppPackage + ":id/wine_name");

        public AndroidElement BtnBack => driver.FindElementByXPath("//android.widget.ImageButton[@content-desc='Navigate up']");

    }
}
