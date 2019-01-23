using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models.Enum
{
    public enum AccountType
    {
        [Display(Name = "حقیقی")]
        Person = 0,
        [Display(Name = "حقوقی")]
        Legal = 1,
    }
}
