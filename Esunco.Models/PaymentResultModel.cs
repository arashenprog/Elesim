using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class PaymentResultModel
    {
        public long OrderID { get; set; }
        public string PaymentID { get; set; }
        public string PaymentUrl { get; set; }
    }
}
