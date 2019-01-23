
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Elesim.Shared;
using AcoreX.Xamarin.Droid;
using System.Threading;
using Esunco.Models;
using System;
using Android.Runtime;

namespace Elesim.Droid.Code.UI
{
    [Activity(Label = "RegisterActivity", Theme = "@style/LoginTheme")]
    public class RegisterActivity : BaseActivity
    {

        TextView tbxMobile;
        ProgressDialog progressDialog;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_register);
            //
            tbxMobile = FindViewById<TextView>(Resource.Id.tbxNumber);
            var tbxFirstName = FindViewById<TextView>(Resource.Id.tbxFirstName);
            var tbxLastName = FindViewById<TextView>(Resource.Id.tbxLastName);
            var tbxNationalCode = FindViewById<TextView>(Resource.Id.tbxNationalCode);

            FindViewById<Button>(Resource.Id.btnLogin).Click += delegate
            {
                progressDialog = new ProgressDialog(this, ProgressDialog.ThemeDeviceDefaultLight);
                progressDialog.SetMessage("لطفا کمی صبر کنید...");
                RunOnUiThread(() => progressDialog.Show());
                new Thread(new ThreadStart(delegate
                {
                    var c = new ClientProfileServiceModel()
                    {
                        Mobile = tbxMobile.Text.Trim(),
                        Firstname = tbxFirstName.Text.Trim(),
                        Lastname = tbxLastName.Text.Trim(),
                        NationalCode = tbxNationalCode.Text.Trim()
                    };

                    try
                    {
                        Facade.Register(c);
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
                        RunOnUiThread(() =>
                        {
                            progressDialog.Hide();
                        });
                    }

                })).Start();
            };

            var btnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            if (btnSignUp != null)
            {
                btnSignUp.Click += delegate
                {
                    StartActivity(typeof(LoginActivity));
                    Finish();
                };
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
        protected override void OnPause()
        {
            base.OnPause();
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