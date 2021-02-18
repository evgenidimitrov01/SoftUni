using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Vivino_AndroidSoftware;
using Vivino_AndroidSoftware.Activities;

namespace Appium_7zip_Vivino_Homework
{
    [TestFixture]
    class Test_VivinoSignIn
    {
        private const string VivinoAppStartActivity = "com.sphinx_solution.activities.SignInActivity";

        [OneTimeSetUp]
        public void Setup()
        {
            Driver.InitializeDriver(VivinoAppStartActivity);
        }

        [Category("Vivino_InvalidEmailFromFile")]
        [TestCaseSource("LoadTestDataFromTxtFile")]
        public void Test_VivinoLoginInvalidData(string email, string password, string expected)
        {
            //Login
            SignInActivity signInAct = new SignInActivity(Driver.driver);
            signInAct.FillLoginData(email, password);

            Assert.AreEqual(signInAct.AlertMessage.Text, expected);

            signInAct.BtnAlertOK.Click();
            Thread.Sleep(1000);
        }

        static IEnumerable<TestCaseData> LoadTestDataFromTxtFile()
        {
            string[] lines = File.ReadAllLines("InvalidEmailData.txt");

            foreach (string line in lines)
            {
                string[] lineItems = line.Split(';');
                string email = lineItems[0];
                string password = lineItems[1];
                string expected = lineItems[2];
                yield return new TestCaseData(email, password, expected);
            }
        }

        [Category("Vivino_ValidEmail_InvalidPassword")]
        //Valid not existing email
        [TestCase("evg@eni.bg", "evgeni", "The email does not exist")]
        [TestCase("West@west.west", "west", "The email does not exist")]
        [TestCase("az@nqmam.email", "nqmamparola", "The email does not exist")]
        [TestCase("some@email.bg", "SoMePaSsWoRd", "The email does not exist")]
        //Valid email, not valid password
        [TestCase("evgeni@abv.bg", "xaxaxa", "The password is not correct!")]
        [TestCase("evgeni@abv.bg", "asdasd", "The password is not correct!")]
        [TestCase("evgeni@abv.bg", "wwwwwww", "The password is not correct!")]
        [TestCase("evgeni@abv.bg", "!@#$%^", "The password is not correct!")]
        public void Test_VivinoLoginValidNotExistingData(string email, string password, string expectedMessage)
        {
            //Login
            SignInActivity signInAct = new SignInActivity(Driver.driver);
            signInAct.FillLoginData(email, password);

            WebDriverWait wait = new WebDriverWait(Driver.driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.TextToBePresentInElement(signInAct.ErrorMessage, expectedMessage));
            Assert.AreEqual(signInAct.ErrorMessage.Text, expectedMessage);
        }

        [OneTimeTearDown]
        public void ShutDown()
        {
            Driver.driver.Quit();
        }
    }
}
