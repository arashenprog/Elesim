using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class ReceiptModel
    {
        public DateTime Date { get; set; }
        public DateTime? ExpireDate { get; set; }

        public OrderItemType Type { get; set; }
        public SimType SimType { get; set; }
        public string ActivationCode { get; set; }
        public string PaymentCode { get; set; }
        public string ReceiptCode { get; set; }
        public string TraceCode { get; set; }
        public string Password { get; set; }
        public long OrderID { get; set; }
        public long Price { get; set; }
        public string Title { get; set; }
        public string ClientName { get; set; }
        public string ClientMobile { get; set; }
        public string ElesimContact { get; set; }
        public string DownloadUrl { get; set; }
    }
}
