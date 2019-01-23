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
using Elesim.Droid.Code.Adapters;
using Android.Support.V4.Widget;
using System.Threading.Tasks;
using Esunco.Models;
using AcoreX.Helper;
using AcoreX.Xamarin.Droid.UI;

namespace Elesim.Droid.Code.UI
{

    [Activity(Label = "تاریخچه خرید", Theme = "@style/AppTheme.NoActionBar")]
    public class OrderHistoryActivity : BaseActivity
    {


        RecyclerView recyclerView;
        OrderHistoryAdapter adapter;
        SwipeRefreshLayout swipeRefreshLayout;
        long lastLoadedId = 0;
        Android.Support.V7.Widget.Toolbar toolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_order_history);
            //
            //Toolbar
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "تاریخچه خرید";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            
            //
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            var layoutManager = new GridLayoutManager(this, 1, GridLayoutManager.Vertical, false);
            var onScrollListener = new RecyclerViewOnScrollListener(layoutManager);
            onScrollListener.LoadMoreEvent += onScrollListener_LoadMoreEvent;
            recyclerView.AddOnScrollListener(onScrollListener);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetItemAnimator(new DefaultItemAnimator());
            adapter = new OrderHistoryAdapter();
            recyclerView.SetAdapter(adapter);
            adapter.OnItemClick += Adapter_OnItemClick;
            //
            swipeRefreshLayout.SetColorSchemeColors(Resource.Color.colorPrimary, Resource.Color.colorGreen, Resource.Color.colorOrange);
            swipeRefreshLayout.Refresh += SwipeRefreshLayout_Refresh;
        }


        private async void SwipeRefreshLayout_Refresh(object sender, EventArgs e)
        {
            await Reload();
        }

        private void Adapter_OnItemClick(object sender, ItemClickEventArgs<Esunco.Models.OrderHistoryModel> e)
        {
            var intent = new Android.Content.Intent(this, typeof(OrderDetailActivity));
            intent.PutExtra("ID", e.Item.ID);
            this.StartActivity(intent);
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



        private async Task Reload()
        {
            lastLoadedId = 0;
            adapter.Clear();
            await LoadMore();
        }



        protected override async void OnResume()
        {
            base.OnResume();
            await Reload();
        }

        

        async void onScrollListener_LoadMoreEvent(object sender, EventArgs e)
        {
            await LoadMore();
        }

        private async Task LoadMore()
        {
            try
            {
                RunOnUiThread(() =>
                {
                    swipeRefreshLayout.Refreshing = true;
                });
                var list = await Facade.GetOrderHistory(lastLoadedId);
                if (list.Any())
                    lastLoadedId = list.Last().ID;

                this.adapter.AddItems(list);
                this.adapter.NotifyDataSetChanged();
                ThreadHelper.ExecuteTaskWithDelay(500, () =>
                {
                    RunOnUiThread(() =>
                    {
                        swipeRefreshLayout.Refreshing = false;
                    });

                });
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
        }
    }
}