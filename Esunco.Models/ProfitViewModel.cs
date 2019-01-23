using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class ProfitViewModel
    {
        [Display(Name = "مجموع فروش")]
        public long TotalSell { get; set; }

        [Display(Name = "سرمایه خرید")]
        public long TotalBuy { get; set; }

        [Display(Name = "سود")]
        public long Profit { get; set; }
    }
}
