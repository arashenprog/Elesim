using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models.Enum
{
    public enum PaymentStatus
    {
        [Display(Name = "در انتظار تایید")]
        Sent = 0,
        [Display(Name = "در انتظار تایید")]
        Verified = 1,
        [Display(Name = "پرداخت شده")]
        Settled = 2,
        [Display(Name = "منقضی شده")]
        Expired = 8,
        [Display(Name = "ناموفق")]
        Failed = 9,
    }
}
