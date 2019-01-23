using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class PackModel : BaseModel
    {

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "قیمت")]
        public long Price { get; set; }

        [Display(Name = "تاریخ اضافه شدن")]
        public DateTime CreateTime { get; set; }


        [Display(Name = "پیش شماره")]
        public string Code { get; set; }

        [Display(Name = "وضعیت")]
        public SimPackStatus Status { get; set; }


       [Display(Name = "نوع بسته")]
       public PackType Type { get; set; }
    }
}
