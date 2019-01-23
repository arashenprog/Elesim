using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class PaymentCallbackModel
    {
        public string RefId { get; set; }
        public string ResCode { get; set; }
        public long SaleOrderId { get; set; }
        public long SaleReferenceId { get; set; }
    }
}
