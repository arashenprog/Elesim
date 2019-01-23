using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class PaymentReportModel: BaseModel
    {
        [Display(Name = "نام و نام خانوادگی")]
        public string ClientName { get; set; }

        [Display(Name = "موبایل")]
        public string ClientMobile { get; set; }

        [Display(Name = "زمان پرداخت")]
        public DateTime DateTime { get; set; }

        [Display(Name = "مبلغ پرداختی")]
        public long Price { get; set; }

        [Display(Name = "نوع پرداخت")]
        public String Type { get; set; }

        [Display(Name = "شماره درخواست")]
        public long? SaleOrderID { get; set; }

        [Display(Name = "شناسه پرداخت")]
        public string RefID { get; set; }

        [Display(Name = "شناسه تراکنش")]
        public long? SaleRefID { get; set; }

        [Display(Name = "وضعیت پرداخت")]
        public PaymentStatus PaymentStatus { get; set; }
    }
}
