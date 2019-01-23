using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esunco.Models
{
    public class PaymentOrderModel
    {
        public string Token { get; set; }
        public List<OrderItemModel> Items { get; set; }
    }
}
