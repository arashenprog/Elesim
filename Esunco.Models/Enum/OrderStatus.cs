using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models.Enum
{


    public enum OrderStatus
    {
        [Display(Name = "پرداخت نشده")]
        Unpaid = 0,
        [Display(Name = "پرداخت شده")]
        Paid = 1,
        [Display(Name = "معلق")]
        Suspended = 3,
        [Display(Name = "لغو شده")]
        Canceled = 9
    }
}
