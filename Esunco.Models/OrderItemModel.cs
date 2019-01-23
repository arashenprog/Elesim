using Esunco.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class OrderItemModel : BaseModel
    {
        public long ItemID { get; set; }

        public string Title { get; set; }
        public long Price { get; set; }

        public OrderItemType Type { get; set; }
    }
}
