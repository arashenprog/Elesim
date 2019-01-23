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
using Android.Widget;
using AcoreX.DateTimeCalendar;
using Newtonsoft.Json;
using Android.App;
using AcoreX.Xamarin.Droid;
using System.Globalization;
using Android.Text;
using AcoreX.Helper;

namespace Elesim.Droid.Code.UI
{
    [Activity(Label = "جزئیات مزایده", Theme = "@style/AppTheme.NoActionBar")]
    public class AuctionDetailsActivity : BaseActivity
    {
        AuctionServiceModel model;

        System.Threading.Timer _timer;

        TextView _leftTime;
        long totalLeftSeconds;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.fragment_auction_details);

            var str = Intent.GetStringExtra("Data");
            model = JsonConvert.DeserializeObject<AuctionServiceModel>(str);

            FindViewById<TextView>(Resource.Id.tbxNumber).Text = String.Join("\n", model.Numbers);
            FindViewById<TextView>(Resource.Id.tbxTitle).Text = model.Title;
            FindViewById<TextView>(Resource.Id.tbxPrice).Text = String.Format("قیمت پایه: {0} ریال", model.BasePrice.ToString("#,###"));
            FindViewById<TextView>(Resource.Id.tbxTopPrice).Text = model.MaxPrice > 0 ? String.Format("بالاترین: {0} ریال", model.MaxPrice.ToString("#,###")) : "";
            FindViewById<TextView>(Resource.Id.tbxYourPrice).Text = model.YourPrice > 0 ? String.Format("پیشنهاد شما: {0} ریال", model.YourPrice.ToString("#,###")) : "";

            if (model.IsWinner)
            {
                FindViewById<Button>(Resource.Id.btnBuy).Text = "پرداخت";
                FindViewById(Resource.Id.btnBuy).Click += Buy_Click;
            }
            else
            {
                FindViewById<Button>(Resource.Id.btnBuy).Text = model.YourPrice > 0 ? "ثبت پیشنهاد جدید" : "شرکت در مناقصه";
                FindViewById(Resource.Id.btnBuy).Click += SetBid_Click;
            }

            _timer = new System.Threading.Timer(TimerCallback, null, 0, 1000);
            totalLeftSeconds = model.TotalLeftSeconds;
            _leftTime = FindViewById<TextView>(Resource.Id.tbxLeftTime);
            TimeSpan time = TimeSpan.FromSeconds(totalLeftSeconds);
            _leftTime.Text = time.ToString(@"hh\:mm\:ss");
            UpdateText();
        }

        private void TimerCallback(object state)
        {
            if (_leftTime != null)
            {
                RunOnUiThread(UpdateText);
            }
        }

        private void UpdateText()
        {
            TimeSpan time = TimeSpan.FromSeconds(totalLeftSeconds--);
            if (model != null && model.IsWinner)
                _leftTime.Text = "!تبریک";
            else
                _leftTime.Text = time.ToString(@"hh\:mm\:ss");
        }

        private void SetBid_Click(object sender, EventArgs e)
        {
            if (CheckLogin())
            {
                if (this.model.BasePrice > Facade.Client.Credit)
                {
                    Toast.MakeText(this, "مبلغ اعتبار حساب شما از قیمت پایه کمتر است.", ToastLength.Long).Show();
                    return;
                }
                var builder = new Android.App.AlertDialog.Builder(this);
                View view = LayoutInflater.Inflate(Resource.Layout.layout_bid, null);
                builder.SetView(view);
                Android.App.AlertDialog dialog = builder.Create();
                var tbxPrice = view.FindViewById<EditText>(Resource.Id.tbxPrice);
                tbxPrice.AfterTextChanged += EditText_AfterTextChanged;


                //
                view.FindViewById<View>(Resource.Id.btnSubmit).Click += delegate
                {
                    var price = tbxPrice.Text.Replace(",", "").DefaultIfNull<long>(0);
                    if (price <= 0)
                    {
                        Toast.MakeText(this, "مبلغ وارده صحیح نمی باشد.", ToastLength.Long).Show();
                        return;
                    }
                    dialog.Hide();
                    ShowLoading(() =>
                    {
                        if (Facade.SetAuctionBid(model.ID, price))
                        {
                            RunOnUiThread(() =>
                            {
                                FindViewById<TextView>(Resource.Id.tbxYourPrice).Text = model.YourPrice > 0 ? String.Format("پیشنهاد شما: {0} ریال", price.ToString("#,###")) : "";
                            });
                        };
                    });
                };
                dialog.Show();
            }
        }

        private void EditText_AfterTextChanged(object sender, AfterTextChangedEventArgs e)
        {
            var editText = sender as EditText;
            editText.AfterTextChanged -= EditText_AfterTextChanged;
            var text = e.Editable.ToString();
            int value = 0;
            int.TryParse(text.Replace(",", ""), out value);
            editText.Text = string.Format("{0:#,###}", value);
            editText.SetSelection(editText.Text.Length);
            editText.AfterTextChanged += EditText_AfterTextChanged;
        }

        private void Buy_Click(object sender, EventArgs e)
        {
            if (CheckLogin())
            {
                ShowPayment(new OrderItemModel { ItemID = model.ID, Price = (model.YourPrice - model.BasePrice), Title = model.Title, Type = Esunco.Models.Enum.OrderItemType.Auction });
            }
        }

    }
}