using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class BidModel : BaseModel
    {
        [Display(Name = "قیمت")]
        public long Price { get; set; }

     

        [Display(Name = "زمان پیشنهاد")]
        public DateTime Time { get; set; }



        [Display(Name = "خریدار")]
        public ClientModel Client { get; set; }

    }
}
