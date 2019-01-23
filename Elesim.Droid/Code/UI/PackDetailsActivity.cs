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
using AcoreX.DateTimeCalendar;
using Newtonsoft.Json;

namespace Elesim.Droid.Code.UI
{
    [Activity(Label = "جزئیات پک", Theme = "@style/AppTheme.NoActionBar")]
    public class PackDetailsActivity : BaseActivity
    {
        PackServiceModel model;
        Android.Support.V7.Widget.Toolbar toolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_pack_details);
            //
            var str = Intent.GetStringExtra("Pack");
            model = JsonConvert.DeserializeObject<PackServiceModel>(str);
            //
            //Toolbar
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = model.Title;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            //
            FindViewById<TextView>(Resource.Id.tbxNumber).Text = String.Join("\n", model.Numbers);
            FindViewById<TextView>(Resource.Id.tbxTitle).Text = model.Title;
            FindViewById<TextView>(Resource.Id.tbxPrice).Text = String.Format("{0:#,###} ريال", model.Price);
            FindViewById<TextView>(Resource.Id.tbxTime).Text = model.CreateTime.ToPersian().ToPrettyDate();
            FindViewById(Resource.Id.btnBuy).Click += PackDetailsFragment_Click;

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void PackDetailsFragment_Click(object sender, EventArgs e)
        {
            if (CheckLogin() && CheckAccountType() && CheckProfile() && CheckEmail())
            {

                ShowPayment(new OrderItemModel { ItemID = model.ID, Price = model.Price, Title = model.Title, Type = Esunco.Models.Enum.OrderItemType.Pack });
            }
        }
    }
}