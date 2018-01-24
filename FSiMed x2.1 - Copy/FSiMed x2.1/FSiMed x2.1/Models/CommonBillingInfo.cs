using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSiMed_x2._1.Models
{
    public class CommonBillingInfo
    {
        public int Id { get; set; } = 0;
        public string PatientId { get; set; }
        public int VisitNo { get; set; }
        public double TotalAmt { get; set; }
        public double Discount { get; set; }
        public double NetAmount { get; set; }
        public string Guid { get; set; }
        public List<CommonBillingDetails> Details { get; set; }

    }
}