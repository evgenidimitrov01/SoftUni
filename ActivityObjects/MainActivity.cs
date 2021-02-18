using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System.Collections.ObjectModel;

namespace Vivino_AndroidSoftware.Activities
{
    public class MainActivity
    {
        AndroidDriver<AndroidElement> driver;

        public MainActivity(AndroidDriver<AndroidElement> driver)
        {
            this.driver = driver;
        }

        public AndroidElement BtnTopCharts => driver.FindElementById(Helpers.VivinoAppPackage + ":id/top_list_tab");
        public AndroidElement BtnSearch => driver.FindElementById(Helpers.VivinoAppPackage + ":id/wine_explorer_tab");
        public AndroidElement BtnFeeds => driver.FindElementById(Helpers.VivinoAppPackage + ":id/feed_tab");
        public AndroidElement BtnMyProfile => driver.FindElementById(Helpers.VivinoAppPackage + ":id/my_profile_tab");
        public AndroidElement BtnSearchBox => driver.FindElementById(Helpers.VivinoAppPackage + ":id/search_vivino");
        public AndroidElement BtnWishList => driver.FindElementById(Helpers.VivinoAppPackage + ":id/wishlist");
        public AndroidElement TxtSearchBoxInput => driver.FindElementById(Helpers.VivinoAppPackage + ":id/editText_input");
        public AndroidElement ListSearchedItems => driver.FindElementById(Helpers.VivinoAppPackage + ":id/listviewWineListActivity");

        public ReadOnlyCollection<AppiumWebElement> GetSearchedResults()
        {
            return ListSearchedItems.FindElementsById("listviewWineListActivity");
        }
    }
}
