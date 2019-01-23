using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models.Enum
{
    public enum PaymentType
    {
        [Display(Name = "درگاه بانکی")]
        BankGateway = 0,
        [Display(Name = "اعتبار حساب")]
        Credit = 1,
    }
}
