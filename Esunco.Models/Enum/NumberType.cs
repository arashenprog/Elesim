using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models.Enum
{
    public enum NumberType
    {
        [Display(Name = "عادی")]
        Normal = 0,
        [Display(Name = "رند")]
        Rond = 1
    }
}
