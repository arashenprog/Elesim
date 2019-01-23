using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class OrderReportModel : BaseModel
    {

        [Display(Name = "شماره فاکتور")]
        public new long ID { get; set; }

        [Display(Name = "نام و نام خانوادگی")]
        public string ClientFullname { get; set; }


        [Display(Name = "وضعیت سفارش")]
        public OrderStatus OrderStatus { get; set; }


        [Display(Name = "موبایل")]
        public string ClientMobile { get; set; }

        [Display(Name = "کد ملی")]
        public string ClientNationalCode { get; set; }

        [Display(Name = "کد دفتر خدماتی")]
        public string ClientOfficeCode { get; set; }

        [Display(Name = "عنوان")]
        public string Title
        {
            get
            {
                return String.Join(" / ", this.Items.Select(c => c.Title));
            }
        }




        [Display(Name = "شماره درخواست")]
        public long? SaleOrderID { get; set; }

        [Display(Name = "زمان درخواست")]
        public DateTime OrderTime { get; set; }


        [Display(Name = "شناسه پرداخت")]
        public string RefID { get; set; }

        [Display(Name = "شناسه تراکنش")]
        public long? SaleRefID { get; set; }


        [Display(Name = "کد خطای تراکنش")]
        public string ResultCode { get; set; }

        [Display(Name = "مبلغ فاکتور")]
        public long Price { get; set; }

        [Display(Name = "وضعیت پرداخت")]
        public PaymentStatus PaymentStatus { get; set; }


        public List<OrderItemModel> Items { get;  set; }

    }
}
