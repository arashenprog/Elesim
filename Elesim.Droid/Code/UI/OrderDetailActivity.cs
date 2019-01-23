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
using Android.Webkit;
using Org.Apache.Http.Util;
using AcoreX.Xamarin.Droid.UI;

namespace Elesim.Droid.Code.UI
{
    [Activity(Label = "OrderDetailActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class OrderDetailActivity : BaseActivity, IDownloadListener
    {
        WebView webview;
        ProgressDialog loadingDialog;

        Android.Support.V7.Widget.Toolbar toolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_order);
            //Toolbar
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "جزئیات سفارش";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            var orderID = Intent.GetLongExtra("ID", 0);

            webview = FindViewById<WebView>(Resource.Id.webview);
            string postData = String.Format("Token={0}&OrderID={1}", Facade.Client.Token, orderID);
            webview.Settings.JavaScriptEnabled = true; ;
            webview.Settings.JavaScriptCanOpenWindowsAutomatically = false;
            webview.Settings.SetSupportMultipleWindows(false);
            var client = new CustomWebViewClient();
            client.OnPageFinish += Client_OnPageFinish;
            webview.SetWebViewClient(client);
            webview.SetWebChromeClient(new WebChromeClient());
            loadingDialog = ShowLoading();
            webview.PostUrl(Facade.BaseUrl + "/Order/Receipt", EncodingUtils.GetBytes(postData, "BASE64"));
            webview.Settings.AllowFileAccessFromFileURLs = true;
            webview.Settings.AllowFileAccess = true;
            webview.SetDownloadListener(this);

        }

        public void OnDownloadStart(string url, string userAgent, string contentDisposition, string mimetype, long contentLength)
        {
            DownloadManager.Request request = new DownloadManager.Request(Android.Net.Uri.Parse(url));

            request.SetMimeType(mimetype);
            request.AddRequestHeader("User-Agent", userAgent);
            request.SetDescription("Downloading file...");
            request.SetTitle(URLUtil.GuessFileName(url, contentDisposition, mimetype));
            request.AllowScanningByMediaScanner();
            request.SetNotificationVisibility(DownloadVisibility.VisibleNotifyCompleted);
            request.SetDestinationInExternalPublicDir(Android.OS.Environment.DirectoryDownloads, URLUtil.GuessFileName(url, contentDisposition, mimetype));
            DownloadManager dm = (DownloadManager)GetSystemService(DownloadService);
            dm.Enqueue(request);
            Toast.MakeText(this, "دانلود فایل...", ToastLength.Long).Show();
        }

        private void Client_OnPageFinish(object sender, EventArgs e)
        {
            loadingDialog.Hide();
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.order_detail, menu);
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
                case Resource.Id.action_download:
                    Finish();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }


    }
}