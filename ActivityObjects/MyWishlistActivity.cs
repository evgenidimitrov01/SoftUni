using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System.Collections.ObjectModel;

namespace Vivino_AndroidSoftware.Activities
{
    public class MyWishlistActivity
    {
        AndroidDriver<AndroidElement> driver;

        public MyWishlistActivity(AndroidDriver<AndroidElement> driver)
        {
            this.driver = driver;
        }

        public AndroidElement ListWishlist => driver.FindElementById(Helpers.VivinoAppPackage + ":id/wine_list");

        public ReadOnlyCollection<AppiumWebElement> GetAllWishlistElements()
        {
            return ListWishlist.FindElementsByXPath("//*[@class='android.widget.FrameLayout']");
        }
    }
}
