using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class SimCodeViewModel : BaseModel
    {
        [Display(Name = "پیش شماره")]
        public string Number { get; set; }
        [Display(Name = "کد")]
        public string Code { get; set; }

        [Display(Name = "تعداد بارگزاری")]
        public long Loaded { get; set; }
        [Display(Name = "تعداد فروخته شده")]
        public long Sold { get; set; }

        [Display(Name = "موجودی")]
        public long Stock { get; set; }

        [Display(Name = "مبلغ فروش")]
        public long Amount { get; set; }
    }
}
