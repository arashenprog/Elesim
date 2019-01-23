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
using System.IO;
using Android.Webkit;
using Org.Apache.Http.Util;

namespace Elesim.Droid.Code.UI
{

    [Activity(Label = "درباره اِلِسیم", Theme = "@style/AppTheme.NoActionBar")]
    public class AboutActivity : BaseActivity
    {
        Android.Support.V7.Widget.Toolbar toolbar;

        WebView webview;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_about);
            //
            //Toolbar
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "درباره اِلِ سیم";
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            //
            //FindViewById<TextView>(Resource.Id.tbxAbout).Text = ReadFromAssets();

            webview = FindViewById<WebView>(Resource.Id.webview);
          
            webview.Settings.JavaScriptEnabled = true; ;
            webview.Settings.JavaScriptCanOpenWindowsAutomatically = false;
            webview.Settings.SetSupportMultipleWindows(false);
            var client = new CustomWebViewClient();
            client.OnPageError += Client_OnPageError;
            webview.SetWebViewClient(client);
            webview.SetWebChromeClient(new WebChromeClient());
            //loadingDialog = ShowLoading();
            webview.LoadUrl(Facade.BaseUrl + "/About");
            
        }

        private void Client_OnPageError(object sender, EventArgs e)
        {
            webview.LoadData(ReadFromAssets(), "text/html; charset=UTF-8", null);
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

        public  string ReadFromAssets()
        {
            var stream = this.Assets.Open("about.html");
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }



      
    }
}