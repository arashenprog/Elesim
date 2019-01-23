using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models.Enum
{
    public enum SimPackStatus
    {
        [Display(Name = "منتشر نشده")]
        New = 0,
        [Display(Name = "منتشر شده")]
        Published = 1,
        [Display(Name = "در انتظار تایید")]
        Waiting = 2,
        [Display(Name = "فروخته شده")]
        Sold = 3
    }
}
