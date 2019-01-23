using Esunco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elesim.Shared
{
    public class ShoppingCart
    {
        public List<OrderItemModel> Items { get; private set; }

        public ShoppingCart()
        {
            this.Items = new List<OrderItemModel>();
        }


        public int Count
        {
            get
            {
                return Items.Count;
            }
        }


        public long TotalPrice
        {
            get
            {
                return Items.Sum(c => c.Price);
            }
        }

        public void Add(OrderItemModel item)
        {
            if (!Items.Any(c => c.ItemID == item.ID))
                this.Items.Add(item);
        }

        public void Remove(OrderItemModel item)
        {
            this.Items.Remove(item);
        }

        public void Clear()
        {
            this.Items.Clear();
        }
    }
}
