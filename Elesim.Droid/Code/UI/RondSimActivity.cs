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
using System.Timers;

namespace Elesim.Droid.Code.UI
{

    [Activity(Label = "سیم کارت های رند", Theme = "@style/AppTheme.NoActionBar")]
    public class RondSimActivity : BaseActivity
    {
        RecyclerView recyclerView;
        ReqularSimAdapter adapter;
        long lastLoadedId = 0;
        Android.Support.V7.Widget.Toolbar toolbar;

        SwipeRefreshLayout swipeRefresh;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_normal_sim);
            //
            //Toolbar
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "سیم کارت های رند";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            //
            swipeRefresh = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            swipeRefresh.Refresh += (s, e) => { Reload(); };

            //
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            var layoutManager = new GridLayoutManager(this, 2, GridLayoutManager.Vertical, false);
            var onScrollListener = new RecyclerViewOnScrollListener(layoutManager);
            onScrollListener.LoadMoreEvent += onScrollListener_LoadMoreEvent;
            recyclerView.AddOnScrollListener(onScrollListener);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetItemAnimator(new DefaultItemAnimator());
            adapter = new RondSimAdapter();
            recyclerView.SetAdapter(adapter);
            adapter.OnItemClick += Adapter_OnItemClick;
        }



        private void Adapter_OnItemClick(object sender, ItemClickEventArgs<Esunco.Models.SimServiceModel> e)
        {
            this.ShowPayment(
                new OrderItemModel { ItemID = e.Item.ID, Price = e.Item.Price, Title = e.Item.Number, Type = Esunco.Models.Enum.OrderItemType.Sim }
            );
        }


        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.list_menu, menu);
            if (menu != null)
            {
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
                case Resource.Id.action_refresh:
                    Reload();
                    break;
                case Resource.Id.action_search:
                    StartActivity(typeof(SearchSimActivity));
                    break;

            }
            return base.OnOptionsItemSelected(item);
        }



        private void Reload()
        {
            lastLoadedId = 0;
            adapter.Clear();
            LoadMore();
        }



        protected override void OnResume()
        {
            base.OnResume();
            Reload();
        }

        void onScrollListener_LoadMoreEvent(object sender, EventArgs e)
        {
            LoadMore();
        }

        private async void LoadMore()
        {
            try
            {
                RunOnUiThread(() => swipeRefresh.Refreshing = true);
                var list = await GetList(lastLoadedId);
                if (list.Any())
                {
                    recyclerView.StopScroll();
                    lastLoadedId = list.Last().ID;
                    this.adapter.AddItems(list);
                    this.adapter.NotifyDataSetChanged();
                }

            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
            finally
            {
                RunOnUiThread(() => swipeRefresh.Refreshing = false);
            }
        }

        protected virtual async Task<List<SimServiceModel>> GetList(long lastId)
        {
            return await Facade.GetRondSims(lastId);
        }

    }
}