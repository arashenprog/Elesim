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

namespace Elesim.Droid.Code.UI
{

    public class SimViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
    {
        public TextView Number { get; private set; }
        public TextView Title { get; private set; }
        public TextView State { get; private set; }
        public TextView Price { get; private set; }
        public TextView Time { get; private set; }
        public SimViewHolder(View view)
             : base(view)
        {
            Number = view.FindViewById<TextView>(Resource.Id.tbxNumber);
            Price = view.FindViewById<TextView>(Resource.Id.tbxPrice);
            Time = view.FindViewById<TextView>(Resource.Id.tbxTime);
            Title = view.FindViewById<TextView>(Resource.Id.tbxTitle);
        }

    }


    public class AuctionViewHolder : SimViewHolder
    {
        public TextView LeftTime { get; private set; }
        public TextView TopPrice { get; private set; }
        public TextView YourPrice { get; private set; }

        System.Threading.Timer _timer;

        Activity activity;

        long totalLeftSeconds;
        AuctionServiceModel model;
        public AuctionViewHolder(View view)
             : base(view)
        {
            LeftTime = view.FindViewById<TextView>(Resource.Id.tbxLeftTime);
            TopPrice = view.FindViewById<TextView>(Resource.Id.tbxTopPrice);
            YourPrice = view.FindViewById<TextView>(Resource.Id.tbxYourPrice);
            _timer = new System.Threading.Timer(TimerCallback, null, 0, 1000);
        }

        private void TimerCallback(object state)
        {
            if (LeftTime != null && activity != null)
            {
                activity.RunOnUiThread(UpdateText);
            }
        }

        private void UpdateText()
        {
            TimeSpan time = TimeSpan.FromSeconds(totalLeftSeconds--);
            if (model != null && model.IsWinner)
                LeftTime.Text = "!تبریک";
            else
                LeftTime.Text = String.Format("{0:00}:{1:00}:{2:00}", time.TotalHours, time.Minutes, time.Seconds);
        }

        public void SetModel(Activity activity, AuctionServiceModel model)
        {
            this.activity = activity;
            this.model = model;
            //
            if (model.Numbers.Count >= 4)
                Number.Text = String.Join("\n", model.Numbers.Take(4));
            else
                Number.Text = String.Join("\n", model.Numbers) + new String('\n', 4 - model.Numbers.Count);
            //
            totalLeftSeconds = model.TotalLeftSeconds;
            UpdateText();
            //
            Title.Text = model.Title;
            Price.Text = String.Format("قیمت پایه: {0} ریال", model.BasePrice.ToString("#,###"));
            TopPrice.Text = model.MaxPrice > 0 ? String.Format("بالاترین: {0} ریال", model.MaxPrice.ToString("#,###")) : "";
            YourPrice.Text = model.YourPrice > 0 ? String.Format("پیشنهاد شما: {0} ریال", model.YourPrice.ToString("#,###")) : "";
        }
    }
    public class ShoppingCartViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
    {
        public TextView Title { get; private set; }
        public TextView Price { get; private set; }
        public View DeleteButton { get; private set; }
        public ShoppingCartViewHolder(View view)
             : base(view)
        {
            Price = view.FindViewById<TextView>(Resource.Id.tbxPrice);
            Title = view.FindViewById<TextView>(Resource.Id.tbxTitle);
            DeleteButton = view.FindViewById<View>(Resource.Id.btnDelete);
        }

    }


    public class OrderHistoryViewHolder : Android.Support.V7.Widget.RecyclerView.ViewHolder
    {
        public TextView Title { get; private set; }
        public TextView Price { get; private set; }
        public TextView Time { get; private set; }
        public OrderHistoryViewHolder(View view)
             : base(view)
        {
            Price = view.FindViewById<TextView>(Resource.Id.tbxPrice);
            Title = view.FindViewById<TextView>(Resource.Id.tbxTitle);
            Time = view.FindViewById<TextView>(Resource.Id.tbxTime);
        }

    }






}