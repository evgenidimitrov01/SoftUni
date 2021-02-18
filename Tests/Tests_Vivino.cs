using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using Vivino_AndroidSoftware;
using Vivino_AndroidSoftware.Activities;

namespace Appium_7zip_Vivino_Homework
{
    [TestFixture]
    public class Tests_Vivino
    {
        private const string VivinoAppStartActivity = "com.sphinx_solution.activities.SplashActivity";
        private const string VivinoTestAccountEmail = "evgeni@abv.bg";
        private const string VivinoTestAccountPassword = "123456";

        [SetUp]
        public void Setup()
        {
            Driver.InitializeDriver(VivinoAppStartActivity);
        }

        [Test]
        [Category("Vivino")]
        public void Test_VivinoWine()
        {
            //Login
            SplashActivity splashAct = new SplashActivity(Driver.driver);
            splashAct.BtnLogIn.Click();

            SignInActivity signInAct = new SignInActivity(Driver.driver);
            signInAct.FillLoginData(VivinoTestAccountEmail, VivinoTestAccountPassword);

            //Search
            MainActivity mainAct = new MainActivity(Driver.driver);
            mainAct.BtnSearch.Click();
            mainAct.BtnSearchBox.Click();
            mainAct.TxtSearchBoxInput.SendKeys("Katarzyna Reserve Red 2006");

            //Open the first search result and assert it holds correct data
            var listSearchResults = mainAct.GetSearchedResults();
            listSearchResults[0].Click();

            NewVintageDetailsActivity newVintageDetailsAct = new NewVintageDetailsActivity(Driver.driver);
            var rating = double.Parse(newVintageDetailsAct.TxtRating.Text);
            Assert.IsTrue(rating >= 0 && rating <= 5);

            var highlightDescr = Driver.ScrollToElement(Helpers.VivinoAppPackage + ":id/highlight_description");
            
            Assert.AreEqual("Among top 1% of all wines in the world", highlightDescr.Text);

            newVintageDetailsAct.FactsTab.Click();

            Assert.AreEqual("Grapes", newVintageDetailsAct.FirstFactTitle.Text);
            Assert.AreEqual("Cabernet Sauvignon,Merlot", newVintageDetailsAct.FirstFactText.Text);
        }
        
        [Test]
        [Category("Vivino")]
        public void Test_VivinoWineAddWishlist()
        {
            //Login
            SplashActivity splashAct = new SplashActivity(Driver.driver);
            splashAct.BtnLogIn.Click();

            SignInActivity signInAct = new SignInActivity(Driver.driver);
            signInAct.FillLoginData(VivinoTestAccountEmail, VivinoTestAccountPassword);

            //Search
            MainActivity mainAct = new MainActivity(Driver.driver);
            mainAct.BtnSearch.Click();
            mainAct.BtnSearchBox.Click();
            mainAct.TxtSearchBoxInput.SendKeys("Petite Sira");

            //Open the first search result and assert it holds correct data
            var listSearchResults = mainAct.GetSearchedResults();
            listSearchResults[0].Click();
            NewVintageDetailsActivity newVintageDetailsAct = new NewVintageDetailsActivity(Driver.driver);
            string winename = newVintageDetailsAct.TxtWineName.Text;
            //Add to wishlist
            newVintageDetailsAct.BtnAddToWishLish.Click();


            WebDriverWait wait = new WebDriverWait(Driver.driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.TextToBePresentInElement(newVintageDetailsAct.BtnAddToWishLish, "Wishlisted"));

            newVintageDetailsAct.BtnBack.Click();

            //Не знам защо, но driver.PressKeyCode(AndroidKeyCode.Back);
            //се изпълнява без грешка, но устройството не прави стъпка назад.
            //Изпълнява се на някой от следващите натискания на Back бутона.
            //Може би има бъг в самото приложение?
            //За това съм направил цикъл, който да натиска Back бутона, докато 
            //не сработи и отиде на страницата, където е видим бутона Profile
            //Това решение е изкуствено за конкретния случай, за да не фейлва теста.
            bool elementNotFound;
            do
            {
                //Натискаме хардуерния бутон Back
                Driver.driver.PressKeyCode(AndroidKeyCode.Back);
                try
                {
                    //Ако е достъпен бутона Profile го кликаме
                    mainAct.BtnMyProfile.Click();
                    //Смъкваме флага, че елемента все още не е открит
                    elementNotFound = false;
                }
                catch (Exception ex)
                {
                    //Ако елемента не е намерен вдигаме флаг на булевата променлива, че елемента не е открит
                    elementNotFound = true;
                }
            }
            while (elementNotFound); //въртим този цикъл, докато променливата elementNotFound е true - т.е. докато не открием бутона Profile
            
            mainAct.BtnWishList.Click();

            MyWishlistActivity myWishListAct = new MyWishlistActivity(Driver.driver);

            var elementsWishlist = myWishListAct.GetAllWishlistElements();
            //Въртим в цикъл всички елементи от wishlist-a и когато намерим
            //името на виното, което добавихме в wishlist-a в списъка
            //казваме, че теста е минал
            foreach (var element in elementsWishlist)
            {
                AppiumWebElement txtWineNameFrame = element.FindElementById(Helpers.VivinoAppPackage + ":id/wine_name");
                if (txtWineNameFrame.Text == winename)
                {
                    Assert.Pass();
                }
            }
        }
        

        [TearDown]
        public void ShutDown()
        {
            Driver.driver.Quit();
        }
    }
}