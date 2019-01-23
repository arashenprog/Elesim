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

namespace Elesim.Droid.Code.UI
{
    [Activity(Label = "LoginActivity", Theme = "@style/LoginTheme")]
    public class LoginActivity : BaseActivity
    {

        TextView tbxMobile;
        ProgressDialog progressDialog;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_login);
            //
            tbxMobile = FindViewById<TextView>(Resource.Id.tbxNumber);
            FindViewById<Button>(Resource.Id.btnLogin).Click += delegate
            {
                UIHelper.HideKeyboard(this);
                if (string.IsNullOrWhiteSpace(tbxMobile.Text))
                    return;

                progressDialog = new ProgressDialog(this, ProgressDialog.ThemeDeviceDefaultLight);
                progressDialog.SetMessage("لطفا کمی صبر کنید...");
                progressDialog.Show();
                new Thread(new ThreadStart(delegate
                {
                    try
                    {
            
                        Facade.RequestLoginSMS(tbxMobile.Text.Trim());
                        var activity = new Intent(this, typeof(SMSActivity));
                        activity.PutExtra("Mobile", tbxMobile.Text);
                        StartActivityForResult(activity, 9);
                    }
                    catch (Exception ex)
                    {
                        HandleException(ex);
                    }
                    finally
                    {
                        RunOnUiThread(() => progressDialog.Hide());
                    }
                })).Start();
            };

            FindViewById<Button>(Resource.Id.btnSignUp).Click += delegate
            {
                StartActivity(typeof(RegisterActivity));
                Finish();
            };
        }


        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 9 && resultCode == Result.Ok)
            {
                StartActivity(typeof(MainActivity));
                Finish();
            }
        }

    }
}