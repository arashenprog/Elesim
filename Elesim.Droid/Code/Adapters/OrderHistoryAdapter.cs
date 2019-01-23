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
using Esunco.Models;
using Elesim.Droid.Code.UI;
using AcoreX.DateTimeCalendar;

namespace Elesim.Droid.Code.Adapters
{
    public class OrderHistoryAdapter: BaseAdapter<OrderHistoryModel, OrderHistoryViewHolder>
    {
        public OrderHistoryAdapter() : base(Resource.Layout.layout_order_history_item)
        {
        }
        protected override View BindViewHolder(OrderHistoryViewHolder holder, OrderHistoryModel model)
        {

            //
            holder.Title.Text = model.Title;
            //
            holder.Time.Text = model.Time.ToPersian().ToString("HH:mm yy/MM/dd");
            //
            holder.Price.Text = model.Price.ToString("#,###") + " ریال";

            return holder.ItemView;
        }
    }
}