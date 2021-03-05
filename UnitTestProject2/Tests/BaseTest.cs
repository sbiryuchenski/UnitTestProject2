﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using System.IO;
using System.Text;
using OpenQA.Selenium.Interactions;
using System;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using OpenQA.Selenium.IE;

namespace NUnitTestProject1
{
    
    public abstract class BaseTest
    {
        protected IWebDriver driver;
        protected IWebElement tab;
        protected IWebElement movetab;
        protected IWebElement textbox;
        protected IWebElement button;


        [OneTimeSetUp, Order(0)]
        public void Initialization() // Инициализация браузера, страницы и элементов на странице
        {
            string browser = ConfigurationManager.AppSettings.Get("browser");
            string path = Directory.GetCurrentDirectory();
            switch (browser)
            {
                case "chrome":
                    driver = new ChromeDriver(path);
                    break;
                case "ie":
                    driver = new InternetExplorerDriver(path);
                    break;
                default:
                    driver = new ChromeDriver(path);
                    break;

            }
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);// Ожидание загрузки страницы 5 секунд
            SetURL();
            InitPage();
            FillDictionary();
        }
         
        public virtual void InitPage()
        {
        }
        public virtual void SetURL()
        {
            driver.Url = "http://demowebshop.tricentis.com/";
        }
        public virtual void FillDictionary()
        {
        }
        protected virtual string SetPath()// Установка пути к элементу на странице
        {
            string path = "//a[normalize-space(text())='";
            return path;
        }
        protected virtual void SetTab(string i)
        {
            var path = SetPath();
            tab = driver.FindElement(By.XPath(path + i + "']"));
            tab.Click();
        }
        protected virtual void MoveOnTab(string i)
        {
            movetab = driver.FindElement(By.XPath("//a[normalize-space(text())='" + i + "']"));
            Actions move = new Actions(driver);
            move.MoveToElement(movetab).Build().Perform();
        }

        protected virtual void Check(string check)// Проверка соответствия URL
        {
            string url = driver.Url;
            Assert.IsTrue(url.Contains(check), "URL не совпадает с ожидаемым");
        }
        protected void InputText(string box, string input)
        {
            textbox = driver.FindElement(By.XPath("//input[@name='"+ box +"']"));
            textbox.SendKeys(input);
        }
        protected void DeleteText(string box)
        {
            textbox = driver.FindElement(By.XPath("//input[@name='" + box + "']"));
            textbox.Clear();
        }
        public virtual void SetButton()
        {
            button = driver.FindElement(By.XPath("//input[@name='register-button']"));
        }
        [OneTimeTearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}