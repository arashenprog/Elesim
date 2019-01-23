
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
using Newtonsoft.Json;
using System.Timers;

namespace Elesim.Droid.Code.UI
{

    [Activity(Label = "مزایده ها", Theme = "@style/AppTheme.NoActionBar")]
    public class AuctionListActivity : BaseActivity
    {

        RecyclerView recyclerView;
        AuctionAdapter adapter;
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
            SupportActionBar.Title = "مزایده ها";
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
            adapter = new AuctionAdapter(this);
            recyclerView.SetAdapter(adapter);
            adapter.OnItemClick += Adapter_OnItemClick;
            //
        }

        private void Adapter_OnItemClick(object sender, ItemClickEventArgs<AuctionServiceModel> e)
        {
            Intent intent = new Intent(this, typeof(AuctionDetailsActivity));
            var str = JsonConvert.SerializeObject(e.Item);
            intent.PutExtra("Data", str);
            this.StartActivity(intent);
        }



        private void Reload()
        {
            lastLoadedId = 0;
            adapter.Clear();
            LoadMore();
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
            }
            return base.OnOptionsItemSelected(item);
        }


        protected override  void OnResume()
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
                //
                var list = await Facade.GetAuctions(lastLoadedId);
                if (list.Any())
                    lastLoadedId = list.Last().ID;
                //
                this.adapter.AddItems(list);
                this.adapter.NotifyDataSetChanged();
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
    }
}