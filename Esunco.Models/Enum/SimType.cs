using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models.Enum
{
    public enum SimType
    {
        [Display(Name = "اعتباری")]
        PrePaid = 0,
        [Display(Name = "دائمی")]
        PostPaid = 1
    }

    public enum PackType
    {
        [Display(Name = "اعتباری")]
        PrePaid = 0,
        [Display(Name = "دائمی")]
        PostPaid = 1,
        [Display(Name = "ترکیبی")]
        Combinatorial = 2
    }
}
