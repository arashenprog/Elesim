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
    public class AuctionAdapter : BaseAdapter<AuctionServiceModel, AuctionViewHolder>
    {
        Activity activity;
        public AuctionAdapter(Activity activity) : base(Resource.Layout.layout_auction_item)
        {
            this.activity = activity;
        }
        protected override View BindViewHolder(AuctionViewHolder holder, AuctionServiceModel model)
        {
            holder.SetModel(activity, model);
            return holder.ItemView;

        }
    }
}