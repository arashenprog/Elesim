using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using System.Timers;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using System.Threading.Tasks;
using AcoreX.Xamarin.Droid.UI;
using AcoreX.Helper;
using Elesim.Droid.Code.Adapters;

namespace Elesim.Droid.Code.UI
{
    public abstract class BaseListActivity : BaseActivity
    {
        private Timer _timer;
        SwipeRefreshLayout swipeRefreshLayout;
        Android.Support.V7.Widget.Toolbar toolbar;
        RecyclerView recyclerView;
        protected long lastLoadedId = 0;
        protected abstract BaseAdapter Adapter { get; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //
            _timer = new Timer(10000);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
            //
            OnInit();
            //
            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            swipeRefreshLayout.SetColorSchemeColors(Resource.Color.colorPrimary, Resource.Color.colorGreen, Resource.Color.colorOrange);
            swipeRefreshLayout.Refresh += SwipeRefreshLayout_Refresh;
            //
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = this.Title;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            //
            var layoutManager = new GridLayoutManager(this, 2, GridLayoutManager.Vertical, false);
            var onScrollListener = new RecyclerViewOnScrollListener(layoutManager);
            onScrollListener.LoadMoreEvent += onScrollListener_LoadMoreEvent;
            //
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            recyclerView.AddOnScrollListener(onScrollListener);
            recyclerView.SetAdapter(this.Adapter);
            //
            Reload();
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main, menu);
            if (menu != null)
            {
            }
            return base.OnCreateOptionsMenu(menu);
        }


        protected abstract void OnInit();

        protected override  void OnResume()
        {
            base.OnResume();
            Reload();
        }

        void onScrollListener_LoadMoreEvent(object sender, EventArgs e)
        {
            OnLoadMore();
        }

        private  void SwipeRefreshLayout_Refresh(object sender, EventArgs e)
        {
            Reload();
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RunOnUiThread(() =>
            {
                Reload();
            });
        }

        protected override void OnDestroy()
        {
            _timer.Stop();
            base.OnDestroy();
        }

        protected async Task LoadMore()
        {
            try
            {
                RunOnUiThread(() =>
                {
                    swipeRefreshLayout.Refreshing = true;
                });
                await OnLoadMore();
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


        private async void Reload()
        {
            lastLoadedId = 0;
            Adapter.Clear();
            await LoadMore();
        }


        protected abstract Task OnLoadMore();
    }
}