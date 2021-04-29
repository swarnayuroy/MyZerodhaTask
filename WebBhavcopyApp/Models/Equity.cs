using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBhavcopyApp.Models
{
    public class Equity
    {
        public String EquityId { get; set; }
        public String EquityName { get; set; }
        public double OpenValue { get; set; }
        public double HighValue { get; set; }
        public double LowValue { get; set; }
        public double CloseValue { get; set; }
        public double LastValue { get; set; }
    }
}