using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class SimModel : BaseModel
    {
        [Display(Name = "نوع")]
        public SimType Type { get; set; }

        [Display(Name = "وضعیت")]
        public SimPackStatus Status { get; set; }

        [Display(Name = "شماره")]
        public long Number { get; set; }

        [Display(Name = "قیمت")]
        public long Price { get; set; }

        [Display(Name = "قیمت خرید")]
        public long BuyPrice { get; set; }

        [Display(Name = "قیمت رند")]
        public long? RondPrice { get; set; }

        [Display(Name = "شناسه قبض")]
        public string ReceiptCode { get; set; }

        [Display(Name = "شناسه پرداخت")]
        public string PaymentCode { get; set; }

        [Display(Name = "تاریخ انقضا")]
        public DateTime? ExpireDate { get; set; }

        [Display(Name = "تاریخ تولید")]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "کد فعال سازی")]
        public string ActivationCode { get; set; }

        [Display(Name = "کد پیگیری")]
        public string TraceCode { get; set; }

        [Display(Name = "نوع شماره")]
        public NumberType NumberType
        {
            get
            {
                return RondPrice.HasValue ? NumberType.Rond : NumberType.Normal;
            }
        }



        [Display(Name = "زمان ثبت سیستم")]
        public DateTime RegisterTime { get; set; }



        [Display(Name = "پیش شماره")]
        public string Code { get; set; }

        [Display(Name = "نمایش ویژه")]
        public string Display { get; set; }


    }
}
