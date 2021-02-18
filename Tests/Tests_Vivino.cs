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

            //�� ���� ����, �� driver.PressKeyCode(AndroidKeyCode.Back);
            //�� ��������� ��� ������, �� ������������ �� ����� ������ �����.
            //��������� �� �� ����� �� ���������� ���������� �� Back ������.
            //���� �� ��� ��� � ������ ����������?
            //�� ���� ��� �������� �����, ����� �� ������� Back ������, ������ 
            //�� ������� � ����� �� ����������, ������ � ����� ������ Profile
            //���� ������� � ���������� �� ���������� ������, �� �� �� ������ �����.
            bool elementNotFound;
            do
            {
                //��������� ���������� ����� Back
                Driver.driver.PressKeyCode(AndroidKeyCode.Back);
                try
                {
                    //��� � �������� ������ Profile �� �������
                    mainAct.BtnMyProfile.Click();
                    //�������� �����, �� �������� ��� ��� �� � ������
                    elementNotFound = false;
                }
                catch (Exception ex)
                {
                    //��� �������� �� � ������� ������� ���� �� �������� ����������, �� �������� �� � ������
                    elementNotFound = true;
                }
            }
            while (elementNotFound); //������ ���� �����, ������ ������������ elementNotFound � true - �.�. ������ �� ������� ������ Profile
            
            mainAct.BtnWishList.Click();

            MyWishlistActivity myWishListAct = new MyWishlistActivity(Driver.driver);

            var elementsWishlist = myWishListAct.GetAllWishlistElements();
            //������ � ����� ������ �������� �� wishlist-a � ������ �������
            //����� �� ������, ����� ��������� � wishlist-a � �������
            //�������, �� ����� � �����
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