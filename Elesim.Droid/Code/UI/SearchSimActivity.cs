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

namespace Elesim.Droid.Code.UI
{
    [Activity(Label = "SearchSimActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class SearchSimActivity : BaseActivity
    {
        Android.Support.V7.Widget.Toolbar toolbar;

        RecyclerView recyclerView;
        SearchSimAdapter adapter;
        View searchView;

        private Dictionary<int, string> _provinces;
        EditText tbxCode;
        long lastLoadedId = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_search_sim);
            // Create your application here
            Window.SetSoftInputMode(SoftInput.StateVisible | SoftInput.AdjustResize);
            //Toolbar
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "جستجوی سیم کارت";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            var btn1 = FindViewById<Button>(Resource.Id.btnPrePaid);
            var btn2 = FindViewById<Button>(Resource.Id.btnPostPaid);
            btn1.Selected = true;
            btn1.Click += delegate
             {
                 btn1.Selected = true;
                 btn2.Selected = false;

             };

            btn2.Click += delegate
            {
                btn2.Selected = true;
                btn1.Selected = false;
            };


            tbxCode = FindViewById<EditText>(Resource.Id.tbxCode);


            var combo = FindViewById<Spinner>(Resource.Id.cbxProvince);
            combo.ItemSelected += Combo_ItemSelected;
            _provinces = EnumHelper.GetEnumList<Province>();
            combo.Adapter = new ArrayAdapter<String>(this, Resource.Layout.spinner_item, _provinces.Select(c => c.Value).ToArray());



            recyclerView = this.FindViewById<RecyclerView>(Resource.Id.recycler_view);
            var layoutManager = new GridLayoutManager(this, 2, GridLayoutManager.Vertical, false);
            var onScrollListener = new RecyclerViewOnScrollListener(layoutManager);
            onScrollListener.LoadMoreEvent += onScrollListener_LoadMoreEvent;
            recyclerView.AddOnScrollListener(onScrollListener);
            recyclerView.SetLayoutManager(layoutManager);
            recyclerView.SetItemAnimator(new DefaultItemAnimator());
            adapter = new SearchSimAdapter();
            recyclerView.SetAdapter(adapter);
            adapter.OnItemClick += Adapter_OnItemClick;
            //
            searchView = FindViewById(Resource.Id.searchBar);
            //
            var slider = FindViewById<Xamarin.RangeSlider.RangeSliderControl>(Resource.Id.slider);
            slider.UpperValueChanged += Slider_ValueChanged;
            slider.LowerValueChanged += Slider_ValueChanged;
            slider.SetSelectedMinValue(5000);
            slider.SetSelectedMaxValue(5000000);
            //slider.ShowLabels = false;
            //slider.StepValue = 10000;
            //slider.SetBarHeight(3);
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

        private void Combo_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var item = (Province)_provinces.ElementAt((int)e.Id).Key;
            if (item == Province.All)
            {
                tbxCode.Text = null;
            }
            else
            {
                tbxCode.Text = ((int)item).ToString();
            }
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
                var list = Facade.SearchSim(new Esunco.Models.Filters.SearchSimFilter
                {
                    LastLoadedId = this.lastLoadedId,
                    SimType = FindViewById<Button>(Resource.Id.btnPrePaid).Selected ? SimType.PrePaid : SimType.PostPaid,
                    PreCode = tbxCode.Text.Trim(),
                    Num4 = FindViewById<EditText>(Resource.Id.tbxNum4).Text.Trim(),
                    Num5 = FindViewById<EditText>(Resource.Id.tbxNum5).Text.Trim(),
                    Num6 = FindViewById<EditText>(Resource.Id.tbxNum6).Text.Trim(),
                    Num7 = FindViewById<EditText>(Resource.Id.tbxNum7).Text.Trim(),
                    Num8 = FindViewById<EditText>(Resource.Id.tbxNum8).Text.Trim(),
                    Num9 = FindViewById<EditText>(Resource.Id.tbxNum9).Text.Trim(),
                    Num10 = FindViewById<EditText>(Resource.Id.tbxNum10).Text.Trim(),
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


        private void Adapter_OnItemClick(object sender, ItemClickEventArgs<Esunco.Models.SimServiceModel> e)
        {
            ShowPayment(
                new OrderItemModel { ItemID = e.Item.ID, Price = e.Item.Price, Title = e.Item.Number, Type = Esunco.Models.Enum.OrderItemType.Sim }
            );
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