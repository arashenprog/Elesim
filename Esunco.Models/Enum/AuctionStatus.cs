
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models.Enum
{
    public enum AuctionStatus
    {
        [Display(Name = "منتشر نشده")]
        Created = 0,
        [Display(Name = "منتشر شده")]
        Published = 1,
        [Display(Name = "شروع شده")]
        Started = 2,
        [Display(Name = "تمام شده")]
        Finished = 3,
        [Display(Name = "در انتظار تایید")]
        Waiting = 4,
        [Display(Name = "فروخته شده")]
        Sold = 5,
        [Display(Name = "لغو شده")]
        Canceled = 8,
    }
}
