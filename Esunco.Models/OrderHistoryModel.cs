using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class OrderHistoryModel : BaseModel
    {
        public PaymentType PaymentType { get; set; }
        public string Title { get; set; }

        public DateTime Time { get; set; }

        public long Price { get; set; }
    }
}
