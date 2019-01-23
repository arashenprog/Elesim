using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class ClientModel : BaseModel
    {

        [Display(Name = "نام و نام خانوادگی")]
        public string Fullname
        {
            get
            {
                return String.Format("{0} {1}", Firstname, Lastname);
            }
        }




        [Display(Name = "نام")]
        public string Firstname { get; set; }

        [Display(Name = "فامیل")]
        public string Lastname { get; set; }

        [Display(Name = "موبایل")]
        public string Mobile { get; set; }

        [Display(Name = "تلفن ثابت")]
        public string Phone { get; set; }

        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "کد پستی")]
        public string PostalCode { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "کد دفتر خدماتی")]
        public string OfficeCode { get; set; }

        [Display(Name = "لیست سیاه")]
        public YesNo BlackList { get; set; }

        [Display(Name = "اعتبار")]
        public long Credit { get; set; }


        [Display(Name = "نوع حساب")]
        public AccountType AccountType { get; set; }

    }
}
