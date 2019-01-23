using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Org.Apache.Http.Util;
using Android.Webkit;
using Android.App;
using Android.Support.V7.App;
using Android.Graphics;
using Elesim.Shared;
using AcoreX.Xamarin.Droid.UI;
using Esunco.Models.Enum;
using AcoreX.Helper;
using System.Threading;

namespace Elesim.Droid.Code.UI
{
    [Activity(Label = "BankGatewayActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class BankGatewayActivity : BaseActivity
    {
        WebView webview;
        TextView tbxUrl;
        string paymentID;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_bank_gateway);


            webview = FindViewById<WebView>(Resource.Id.webview);
            tbxUrl = FindViewById<TextView>(Resource.Id.tbxUrl);

            string paymentUrl = Intent.GetStringExtra("PaymentUrl");
            paymentID = Intent.GetStringExtra("PaymentID");

            tbxUrl.Text = paymentUrl;

            string postData = String.Format("RefId={0}", paymentID);
            webview.Settings.JavaScriptEnabled = true; ;
            webview.Settings.JavaScriptCanOpenWindowsAutomatically = false;
            webview.Settings.SetSupportMultipleWindows(false);
            var client = new CustomWebViewClient();
            client.OnPageChanged += Client_OnPageChanged;
            webview.SetWebViewClient(client);
            webview.SetWebChromeClient(new WebChromeClient());
            webview.PostUrl(paymentUrl, EncodingUtils.GetBytes(postData, "BASE64"));
        }

        private void Client_OnPageChanged(object sender, string e)
        {
            var url = e;
            if (url.ToLower().Contains("callback"))
            {
                webview.Visibility = ViewStates.Invisible;
                ShowLoading(delegate ()
                {
                    var id = Int64.Parse(url.ToLower().Split(new string[] { "id=" }, StringSplitOptions.RemoveEmptyEntries)[1]);
                    var status = PaymentStatus.Sent;
                    int tryCount = 0;
                    do
                    {
                        status = Facade.GetPaymentStatus(paymentID);
                    } while (status != PaymentStatus.Settled && tryCount++ < 10);
                    if (status == PaymentStatus.Settled)
                    {
                        RunOnUiThread(() =>
                         {
                             Intent resultIntent = new Intent();
                             resultIntent.PutExtra("ID", id);
                             SetResult(Result.Ok, resultIntent);
                             Finish();
                         });
                    }
                    else
                    {
                        RunOnUiThread(() =>
                        {
                            SetResult(Result.Canceled);
                            Finish();
                        });
                    }
                });
            }
        }

        public override void OnBackPressed()
        {
            var builder = new Android.App.AlertDialog.Builder(this);
            builder.SetMessage("مطمئنید میخواهید صفحه پرداخت را در حین عملیات ترک کنید؟");
            builder.SetPositiveButton("بله", (s, ex) =>
            {
                SetResult(Result.Canceled);
                Finish();
            });
            builder.SetNegativeButton("خیر", (s, ex) => { });
            builder.Create().Show();


        }
    }


}