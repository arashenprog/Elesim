using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Elesim.Droid.Code.UI;
using AcoreX.DateTimeCalendar;
using AcoreX.Xamarin.Droid;
using Esunco.Models;

namespace Elesim.Droid.Code.Adapters
{
    public class ShoppingCartAdapter : BaseAdapter<OrderItemModel, ShoppingCartViewHolder>
    {
        Activity activity;

        public event ItemClickEventHandler<OrderItemModel> OnDeleteItemClick;
        public ShoppingCartAdapter(Activity activity) : base(Resource.Layout.layout_cart_item)
        {
            this.activity = activity;
            AddItemViewClickEvent = false;
        }
        protected override View BindViewHolder(ShoppingCartViewHolder holder, OrderItemModel model)
        {
            holder.Price.Text = model.Price.ToString("#,###") + "ريال ";
            holder.Title.Text = model.Title;
            
            if (!holder.DeleteButton.HasOnClickListeners)
            {
                holder.DeleteButton.Click += (sender, e) =>
                {
                    OnDeleteItemClick?.Invoke(this, new ItemClickEventArgs<OrderItemModel> { Item = model });
                };
            };

            return holder.ItemView;
        }
    }
}