using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models.Enum
{
    public enum Role
    {
        [Display(Name = "سیستم")]
        System,
        [Display(Name = "مدیر سیستم")]
        Administrator,
        [Display(Name = "پشتیبانی")]
        Support,
        [Display(Name = "کابر")]
        User
    }
}
