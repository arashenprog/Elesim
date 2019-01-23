using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models.Enum
{
    public enum UserStatus
    {
        [Display(Name="غیر فعال")]
        Disabled = 0,
        [Display(Name = "فعال")]
        Enabled = 1
    }
}
