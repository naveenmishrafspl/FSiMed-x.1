using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSiMed_x2._1.Models
{
    public class CommonBillingDetails
    {
        public string TypeName { get; set; }
        public string TestName { get; set; }
        public int Days { get; set; }
        public double TotalAmt { get; set; }
    }
}