using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models.Enum
{
    public enum YesNo
    {
        [Display(Name="بله")]
        Yes,
        [Display(Name="خیر")]
        No
    }
}
