using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class AuctionModel : BaseModel
    {
        [Display(Name = "قیمت پایه")]
        public long BasePrice { get; set; }

        [Display(Name = "بالاترین پیشنهاد")]
        public long MaxPrice { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "شماره ها")]
        public string Numbers { get; set; }

        [Display(Name = "زمان شروع")]
        public DateTime StartTime { get; set; }

        [Display(Name = "زمان پایان")]
        public DateTime FinishTime { get; set; }

        [Display(Name = "وضعیت انتشار")]
        public AuctionStatus Status { get; set; }


    }
}
