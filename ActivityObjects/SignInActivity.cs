using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace Vivino_AndroidSoftware.Activities
{
    public class SignInActivity
    {
        private AndroidDriver<AndroidElement> driver;

        public SignInActivity(AndroidDriver<AndroidElement> driver)
        {
            this.driver = driver;
        }

        public AndroidElement txtEmail => driver.FindElementById(Helpers.VivinoAppPackage + ":id/edtEmail");
        public AndroidElement txtPassword => driver.FindElementById(Helpers.VivinoAppPackage + ":id/edtPassword");
        public AndroidElement btnLogin => driver.FindElementById(Helpers.VivinoAppPackage + ":id/action_signin");
        public AndroidElement ErrorMessage => driver.FindElementById(Helpers.VivinoAppPackage + ":id/txtEmailOrPasswordWasIncorrect");
        public AndroidElement AlertMessage => driver.FindElement(By.Id("android:id/message"));
        public AndroidElement BtnAlertOK => driver.FindElement(By.Id("android:id/button1"));

        public void FillLoginData(string email, string password)
        {
            txtEmail.Clear();
            txtPassword.Clear();
            txtEmail.SendKeys(email);
            txtPassword.SendKeys(password);
            btnLogin.Click();
        }

    }
}
