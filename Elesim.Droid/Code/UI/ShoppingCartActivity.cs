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
using Elesim.Shared;
using Esunco.Models;
using AcoreX.Helper;
using Android.Support.V7.Widget;
using Elesim.Droid.Code.Adapters;
using Android.Graphics.Drawables;

namespace Elesim.Droid.Code.UI
{
    [Activity(Label = "ShoppingCartActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class ShoppingCartActivity : BaseActivity
    {
        RecyclerView recyclerView;
        ShoppingCartAdapter adapter;
        Android.Support.V7.Widget.Toolbar toolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_shopping_cart);
            //Toolbar
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "سبد خرید";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            //
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            var layoutManager = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetItemAnimator(new DefaultItemAnimator());
            adapter = new ShoppingCartAdapter(this);
            adapter.AddItems(Facade.Cart.Items);
            this.adapter.NotifyDataSetChanged();
            recyclerView.SetAdapter(adapter);
            adapter.OnDeleteItemClick += Adapter_OnItemClick;

            FindViewById<Button>(Resource.Id.btnBuy).Click += ShoppingCartActivity_Click;

        }

        private void ShoppingCartActivity_Click(object sender, EventArgs e)
        {
            var result = Facade.CheckCart();
            var intent = new Intent(this, typeof(BankGatewayActivity));
            intent.PutExtra("PaymentID", result.PaymentID);
            intent.PutExtra("PaymentUrl", result.PaymentUrl);
            StartActivityForResult(intent, 5);
        }

        private void Adapter_OnItemClick(object sender, ItemClickEventArgs<OrderItemModel> e)
        {
            RemoveFromCart( e.Item, () =>
             {
                 adapter.Clear();
                 adapter.AddItems(Facade.Cart.Items);
                 this.adapter.NotifyDataSetChanged();
             });
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.shopping_cart, menu);
            if (menu != null)
            {
                var item = menu.FindItem(Resource.Id.action_clear);
                LayerDrawable icon = (LayerDrawable)item.Icon;
                //
                if (Facade.Cart.Count > 0)
                    Utils2.setBadgeCount(this, icon, Facade.Cart.Count);
            }
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    break;
                case Resource.Id.action_clear:
                    //ClearCart(() =>
                    // {
                    //     Finish();
                    // });
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 5 && resultCode == Result.Canceled)
            {
                Toast.MakeText(this, "عملیات پرداخت انجام نشد!", ToastLength.Long).Show();
            }
            if (requestCode == 5 && resultCode == Result.Ok)
            {
                Toast.MakeText(this, "عملیات پرداخت با موفقیت انجام شد.", ToastLength.Long).Show();
                Facade.Cart.Clear();
                Finish();
            }
        }
    }
}