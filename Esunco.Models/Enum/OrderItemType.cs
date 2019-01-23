using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models.Enum
{
    public enum OrderItemType
    {
        [Display(Name = "سیمکارت")]
        Sim = 0,
        [Display(Name = "پک")]
        Pack = 1,
        [Display(Name = "مزایده")]
        Auction = 2
    }
}
