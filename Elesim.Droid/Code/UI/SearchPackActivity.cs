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
using AcoreX.Helper;
using Esunco.Models.Enum;
using Android.Support.V7.Widget;
using Elesim.Droid.Code.Adapters;
using AcoreX.Xamarin.Droid.UI;
using Esunco.Models;
using System.Threading.Tasks;
using Android.Views.Animations;
using Android.Views.InputMethods;
using Newtonsoft.Json;

namespace Elesim.Droid.Code.UI
{
    [Activity(Label = "SearchPackActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class SearchPackActivity : BaseActivity
    {
        Android.Support.V7.Widget.Toolbar toolbar;

        RecyclerView recyclerView;
        PackAdapter adapter;
        View searchView;
        EditText tbxCode;
        long lastLoadedId = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_search_pack);
            // Create your application here
            Window.SetSoftInputMode(SoftInput.StateVisible | SoftInput.AdjustResize);
            //Toolbar
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "جستجوی پک سیم کارت های همراه اول";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            var btn1 = FindViewById<Button>(Resource.Id.btnPrePaid);
            var btn2 = FindViewById<Button>(Resource.Id.btnPostPaid);
            var btn3 = FindViewById<Button>(Resource.Id.btnCombinatorial);
            btn1.Selected = true;
            btn1.Click += delegate
             {
                 btn1.Selected = true;
                 btn2.Selected = false;
                 btn3.Selected = false;
             };

            btn2.Click += delegate
            {
                btn2.Selected = true;
                btn1.Selected = false;
                btn3.Selected = false;
            };
            btn3.Click += delegate
            {
                btn3.Selected = true;
                btn1.Selected = false;
                btn2.Selected = false;
            };


            tbxCode = FindViewById<EditText>(Resource.Id.tbxCode);


            recyclerView = this.FindViewById<RecyclerView>(Resource.Id.recycler_view);
            var layoutManager = new GridLayoutManager(this, 2, GridLayoutManager.Vertical, false);
            var onScrollListener = new RecyclerViewOnScrollListener(layoutManager);
            onScrollListener.LoadMoreEvent += onScrollListener_LoadMoreEvent;
            recyclerView.AddOnScrollListener(onScrollListener);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetItemAnimator(new DefaultItemAnimator());
            adapter = new PackAdapter();
            recyclerView.SetAdapter(adapter);
            adapter.OnItemClick += Adapter_OnItemClick; ;
            //
            searchView = FindViewById(Resource.Id.searchBar);
            //
            var slider = FindViewById<Xamarin.RangeSlider.RangeSliderControl>(Resource.Id.slider);
            slider.UpperValueChanged += Slider_ValueChanged;
            slider.LowerValueChanged += Slider_ValueChanged;
            slider.SetSelectedMinValue(5000);            
            slider.SetSelectedMaxValue(5000000);
            UpdateSliderText();
            //
            var btnSearch = FindViewById<Button>(Resource.Id.btnSearch);
            btnSearch.Click +=  delegate
              {
                  searchView.Visibility = ViewStates.Gone;
                  lastLoadedId = 0;
                  this.adapter.Clear();
                  LoadMore();
                  try
                  {
                      InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
                      imm.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);
                  }
                  catch (Exception e)
                  {
                      // TODO: handle exception
                  }
                  if (adapter.ItemCount == 0)
                  {
                      searchView.Visibility = ViewStates.Visible;
                      Toast.MakeText(this, "موردی یافت نشد!", ToastLength.Long).Show();
                  }
              };

        }

        private void Slider_ValueChanged(object sender, EventArgs e)
        {
            UpdateSliderText();
        }

        private void UpdateSliderText()
        {
            var slider = FindViewById<Xamarin.RangeSlider.RangeSliderControl>(Resource.Id.slider);
            var max = (long)slider.GetSelectedMaxValue();
            var min = (long)slider.GetSelectedMinValue();
            var str = String.Format("از قیمت {0:#,###} تا قیمت {1:#,###} ریال", min, max);
            FindViewById<TextView>(Resource.Id.sliderText).Text = str;
        }

        private void Adapter_OnItemClick(object sender, ItemClickEventArgs<PackServiceModel> e)
        {
            Intent intent = new Intent(this, typeof(PackDetailsActivity));
            var str = JsonConvert.SerializeObject(e.Item);
            intent.PutExtra("Pack", str);
            this.StartActivity(intent);
        }

        void onScrollListener_LoadMoreEvent(object sender, EventArgs e)
        {
            LoadMore();
        }


        private async void LoadMore()
        {
            try
            {
                var slider = FindViewById<Xamarin.RangeSlider.RangeSliderControl>(Resource.Id.slider);
                var list = Facade.SearchPack(new Esunco.Models.Filters.SearchPackFilter
                {
                    LastLoadedId = this.lastLoadedId,
                    PackType = FindViewById<Button>(Resource.Id.btnPrePaid).Selected ? PackType.PrePaid : (FindViewById<Button>(Resource.Id.btnPostPaid).Selected ? PackType.PostPaid : PackType.Combinatorial),
                    PreCode = tbxCode.Text.Trim(),
                    MaxPrice = (long)slider.GetSelectedMaxValue(),
                    MinPrice = (long)slider.GetSelectedMinValue()
                });
                if (list.Any())
                {
                    lastLoadedId = list.Last().ID;
                    this.adapter.AddItems(list);
                    this.adapter.NotifyDataSetChanged();
                }


            }
            catch (Exception ex)
            {
                this.HandleException(ex);
            }
        }




        public override void OnBackPressed()
        {
            if (searchView.Visibility == ViewStates.Gone)
            {
                Clear();
            }
            else
            {
                base.OnBackPressed();
            }
        }


        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
                case Resource.Id.action_search:
                    Clear();
                    break;

            }
            return base.OnOptionsItemSelected(item);
        }

        private void Clear()
        {
            lastLoadedId = 0;
            this.adapter.Clear();
            searchView.Visibility = ViewStates.Visible;
        }
    }
}