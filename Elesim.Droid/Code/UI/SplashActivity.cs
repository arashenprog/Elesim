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
using AcoreX.Xamarin.Droid;
using Elesim.Shared;
using System.Threading;
using Android.Support.V7.App;
using System.Threading.Tasks;

namespace Elesim.Droid.Code.UI
{
    [Activity(Label = "اِلِ سیم", Theme = "@style/SplashTheme", Icon = "@drawable/ic_launcher", MainLauncher = true)]
    public class SplashActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //var facade = new Facade();
            Task.Factory.StartNew(() =>
            {
                //Facade.UpdateAppInfo();
                var token = AppPrefs.GetValue<string>("Token", null);
                if (token != null)
                {
                    Facade.SignIn(token);
                }
            });
            new Handler().PostDelayed(delegate
            {
                StartActivity(typeof(MainActivity));
                Finish();
            }, 2000);
        }
    }
}