using OpenQA.Selenium.Appium.Android;

namespace Vivino_AndroidSoftware.Activities
{
    public class SplashActivity
    {
        private AndroidDriver<AndroidElement> driver;

        public SplashActivity(AndroidDriver<AndroidElement> driver)
        {
            this.driver = driver;
        }

        public AndroidElement BtnLogIn => driver.FindElementById(Helpers.VivinoAppPackage + ":id/txthaveaccount");
    }
}
