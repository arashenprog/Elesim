using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class ClientOrderViewModel : BaseModel
    {
        [Display(Name = "نام مشتری")]
        public string Name { get; set; }

        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        [Display(Name = "جمع اعتباری")]
        public long PrePaidTotal { get; set; }

        [Display(Name = "تعداد اعتباری")]
        public long PrePaidCount{ get; set; }

        [Display(Name = "جمع دائمی")]
        public long PosdPaidTotal { get; set; }

        [Display(Name = "تعداد دائمی")]
        public long PosdPaidCount { get; set; }

        [Display(Name = "کل تعداد")]
        public long TotalCount { get; set; }

        [Display(Name = "جمع مبلغ")]
        public long Total { get; set; }
    }
}
