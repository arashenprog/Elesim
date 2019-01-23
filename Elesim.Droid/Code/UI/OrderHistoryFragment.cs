using System;
using System.Linq;
using Android.OS;
using Android.Views;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Elesim.Droid.Code.Adapters;
using Elesim.Shared;
using AcoreX.Xamarin.Droid.UI;
using Android.Support.V4.Widget;
using System.Threading.Tasks;
using AcoreX.Helper;
using Esunco.Models;

namespace Elesim.Droid.Code.UI
{
    public class OrderHistoryFragment : BaseFragment
    {
        RecyclerView recyclerView;
        OrderHistoryAdapter adapter;
        SwipeRefreshLayout swipeRefreshLayout;
        long lastLoadedId = 0;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.activity_order_history, container, false);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recycler_view);
            swipeRefreshLayout = view.FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            var layoutManager = new GridLayoutManager(Context, 1, GridLayoutManager.Vertical, false);
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
            swipeRefreshLayout.Refresh += async delegate
            {
                await Reload();
            };
            //
            return view;
        }

        private async Task Reload()
        {
            lastLoadedId = 0;
            adapter.Clear();
            await LoadMore();
        }

        public override async void OnResume()
        {
            base.OnResume();
            await Reload();
        }

        private void Adapter_OnItemClick(object sender, ItemClickEventArgs<Esunco.Models.OrderHistoryModel> e)
        {
            var intent = new Android.Content.Intent(Activity, typeof(OrderDetailActivity));
            intent.PutExtra("ID", e.Item.ID);
            Activity.StartActivity(intent);
        }

        async void onScrollListener_LoadMoreEvent(object sender, EventArgs e)
        {
            await LoadMore();
        }

        private async Task LoadMore()
        {
            Activity.RunOnUiThread(() =>
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
                    Activity.RunOnUiThread(() => swipeRefreshLayout.Refreshing = false);

                });
        }


    }
}

