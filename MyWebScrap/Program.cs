using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO.Compression;
using System.Threading;

namespace MyWebScrap
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName;
            DateTime myInput = DateTime.Now.AddDays(-1);
            if (DownloadFile(myInput, out fileName))
            {
                ExtractFile(fileName);
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Unable to find the file or have faced with some issues, please check the url {fileName}.");
            }                     
        }
        public static bool DownloadFile(DateTime myInput, out string fileName)
        {
            bool val = true;
            string url = @"https://www.bseindia.com/markets/MarketInfo/BhavCopy.aspx";
            IWebDriver driver = new ChromeDriver();            
            try
            {
                driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl(url);
                string day = myInput.ToString("dd");
                string month = myInput.ToString("MMM");
                string year = myInput.ToString("yyyy");

                driver.FindElement(By.Name("ctl00$ContentPlaceHolder1$fdate1")).SendKeys(day);
                driver.FindElement(By.Name("ctl00$ContentPlaceHolder1$fmonth1")).SendKeys(month);
                driver.FindElement(By.Name("ctl00$ContentPlaceHolder1$fyear1")).SendKeys(year);                
                driver.FindElement(By.Name("ctl00$ContentPlaceHolder1$btnSubmit")).Click();
                
                Thread.Sleep(3000);
                driver.FindElement(By.Id("ContentPlaceHolder1_btnHylSearBhav")).Click();
                fileName = "EQ" + myInput.ToString("dd") + myInput.ToString("MM") + myInput.ToString("yy") + "_CSV.ZIP";
                Console.Clear();
                Console.WriteLine($"file {fileName} downloaded successfully");                               
            }
            catch (Exception)
            {
                val = false;
                fileName = url;
                                                
            }
            finally
            {
                driver.Quit();
            }
            return val;
        }
        public static void ExtractFile(string fileName)
        {
            Console.WriteLine("Extracting");
            try
            {
                string zipPath = @"C:\Users\HP\Downloads\" + fileName;
                string extractPath = @"C:\Users\HP\Downloads";
                ZipFile.ExtractToDirectory(zipPath, extractPath);
                Console.WriteLine("Process complete..");
            }
            catch (Exception)
            {
                Console.WriteLine("Some error occurred");
            }            
            
        }
    }
}
