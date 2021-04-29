using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBhavcopyApp.Models;

namespace WebBhavcopyApp.Controllers
{
    public class EquityController : Controller
    {
        // GET: Equity        
        public ActionResult Index()
        {
            List<Equity> myData = null;
            ViewBag.date = $"{DateTime.Now.AddDays(-1).ToString("dd")} {DateTime.Now.AddDays(-1).ToString("MMM")} {DateTime.Now.AddDays(-1).ToString("yyyy")}"; 
            try
            {
                myData = GetEquityList();
            }
            catch (Exception)
            {
                ViewBag.Mssg = @"https://www.bseindia.com/markets/MarketInfo/BhavCopy.aspx";
                return View("Index", myData);
            }            
            return View(myData);
        }
        public static List<Equity> GetEquityList()
        {
            List<Equity> equityList = new List<Equity>();
            DateTime myDate = DateTime.Now.AddDays(-1);
            string fileName = "EQ" + myDate.ToString("dd") + myDate.ToString("MM") + myDate.ToString("yy") + ".csv";
            using (StreamReader myInput = new StreamReader(@"C:\Users\HP\Downloads\" + fileName))
            {
                //We ignore the first header line of csv file, hence had read before getting into the loop
                string headerLine = myInput.ReadLine();

                string row;
                while ((row = myInput.ReadLine()) != null)
                {
                    string eqId = row.Split(',')[0].Trim();
                    string eqName = row.Split(',')[1].Trim();
                    double openVal = Convert.ToDouble(row.Split(',')[4].Trim());
                    double highVal = Convert.ToDouble(row.Split(',')[5].Trim());
                    double lowVal = Convert.ToDouble(row.Split(',')[6].Trim());
                    double closeVal = Convert.ToDouble(row.Split(',')[7].Trim());
                    double lastVal = Convert.ToDouble(row.Split(',')[8].Trim());
                    equityList.Add(new Equity
                    {
                        EquityId = eqId,
                        EquityName = eqName,
                        OpenValue = openVal,
                        HighValue = highVal,
                        LowValue = lowVal,
                        CloseValue = closeVal,
                        LastValue = lastVal
                    });
                }
            }
            return equityList;
        }
    }
}