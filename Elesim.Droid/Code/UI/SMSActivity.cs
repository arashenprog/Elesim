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
using AcoreX.Xamarin.Droid;
using System.Threading;
using Android.Support.Design.Widget;
using Android.Support.Graphics.Drawable;

namespace Elesim.Droid.Code.UI
{
    [Activity(Label = "SMSActivity", Theme = "@style/LoginTheme")]
    public class SMSActivity : BaseActivity, IReceiveSMS
    {

        TextView tbxMobile;
        ProgressDialog progressDialog;
        string mobile;
        System.Timers.Timer timer;
        private int countSeconds = 60;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_sms);
            mobile = Intent.GetStringExtra("Mobile");
            if (String.IsNullOrWhiteSpace(mobile))
            {
                SetResult(Result.Canceled);
                Finish();
            }
            //
            tbxMobile = FindViewById<TextView>(Resource.Id.tbxNumber);
            RegisterReceiver(new SMSReceiver(), new IntentFilter("android.provider.Telephony.SMS_RECEIVED"));
            progressDialog = new ProgressDialog(this, ProgressDialog.ThemeDeviceDefaultLight);
            progressDialog.SetMessage("لطفا کمی صبر کنید...");
            timer = new System.Timers.Timer();
            //Trigger event every second
            timer.Interval = 1000;
            timer.Elapsed += _timer_Elapsed;
            //count down 5 seconds
            timer.Enabled = true;

            FindViewById<Button>(Resource.Id.btnLogin).Click += delegate
            {
                ValidateCode();
            };

            FindViewById<Button>(Resource.Id.btnSignUp).Click += delegate
            {
                SetResult(Result.Canceled);
                Finish();
            };
            //var drawableCompat = VectorDrawableCompat.Create(this.Resources, Resource.Drawable.ic_lock_gray, this.Theme);
            //FindViewById<EditText>(Resource.Id.tbxNumber).SetCompoundDrawables(drawableCompat, null, null, null);
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            countSeconds--;
            RunOnUiThread(() => FindViewById<TextView>(Resource.Id.tbxCountDown).Text = countSeconds.ToString("D2"));
            if (countSeconds == 0)
            {
                SetResult(Result.Canceled);
                timer.Stop();
                Finish();
            }
        }

        private void ValidateCode()
        {
            UIHelper.HideKeyboard(this);
            if (string.IsNullOrWhiteSpace(tbxMobile.Text))
                return;
            progressDialog.Show();
            new Thread(new ThreadStart(delegate
            {
                var token = Facade.GetLoginToken(mobile, Int32.Parse(tbxMobile.Text));
                AppPrefs.PutValue("Token", token);
                RunOnUiThread(() => progressDialog.Hide());
                SetResult(Result.Ok);
                Finish();
            })).Start();
        }


        public void OnReceive(int code)
        {
            tbxMobile.Text = code.ToString();
            ValidateCode();
        }
    }
}