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
using Android.Support.V7.App;
using Elesim.Shared;
using Esunco.Models;
using System.Threading;

namespace Elesim.Droid.Code.UI
{
    public abstract class BaseActivity : AppCompatActivity
    {

        public Facade Facade { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            if (Window.DecorView.LayoutDirection == LayoutDirection.Ltr)
            {
                Window.DecorView.LayoutDirection = LayoutDirection.Rtl;
            }


            Facade = new Facade();

        }

        internal void HandleException(Exception e)
        {
            var msg = e.InnerException != null ? e.InnerException.Message : e.Message;
            RunOnUiThread(() => { Toast.MakeText(this, msg, ToastLength.Long).Show(); });
        }



        protected override void OnResume()
        {
            base.OnResume();
        }


        #region Helpers


        public bool CheckLogin()
        {
            if (!Facade.IsLogin)
            {
                var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                builder.SetMessage("لطفا ابتدا به حساب کاربری خود وارد شوید.");
                builder.SetPositiveButton("ورود/ ثبت نام", (s, ex) =>
                {
                    StartActivity(typeof(LoginActivity));
                    Finish();
                });
                builder.SetNegativeButton("انصراف", (s, ex) => { });
                builder.Create().Show();
                return false;
            }
            return true;
        }

        public bool CheckProfile()
        {
            if (!Facade.Client.IsCompleted)
            {
                var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                builder.SetMessage("لطفا مشخصات خود را تکمیل کنید.");
                builder.SetPositiveButton("تکمیل مشخصات", (s, ex) =>
                {
                    StartActivity(typeof(ProfileActivity));
                    //Finish();
                });
                builder.SetNegativeButton("انصراف", (s, ex) => { });
                builder.Create().Show();
                return false;
            }
            return true;
        }

        public bool CheckEmail()
        {
            if (String.IsNullOrWhiteSpace(Facade.Client.Email))
            {
                var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                builder.SetMessage("لطفا ایمیل خود را در قسمت مشخصات کاربری وارد کنید.");
                builder.SetPositiveButton("تکمیل مشخصات", (s, ex) =>
                {
                    StartActivity(typeof(ProfileActivity));
                    //Finish();
                });
                builder.SetNegativeButton("انصراف", (s, ex) => { });
                builder.Create().Show();
                return false;
            }
            return true;
        }



        public bool CheckAccountType()
        {
            if (Facade.Client.AccountType == Esunco.Models.Enum.AccountType.Person)
            {

                try
                {
                    Facade.UpdateAppInfo();
                }
                finally
                {
                    var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                    builder.SetMessage("امکان خرید پک صرفا برای اشخاص حقوقی (دفاتر خدمات ارتباطی؛دفاتر ict روستایی؛نقاط فروش مجاز همراه اول؛فروشگاه های زنجیره ای همراه اول) امکان پذیر میباشد. لطفا با پشتیبانی تماس بگیرید.");
                    if (Facade.Info != null)
                    {
                        builder.SetPositiveButton("پشتیبانی 1", (s, ex) =>
                        {
                            Intent intent = new Intent(Intent.ActionDial, Android.Net.Uri.Parse("tel:" + Facade.Info.SupportPhone1));
                            intent.SetFlags(ActivityFlags.NewTask);
                            StartActivity(intent);
                        });
                        builder.SetNegativeButton("پشتیبانی 2", (s, ex) =>
                        {
                            Intent intent = new Intent(Intent.ActionDial, Android.Net.Uri.Parse("tel:" + Facade.Info.SupportPhone2));
                            intent.SetFlags(ActivityFlags.NewTask);
                            StartActivity(intent);
                        });
                        builder.SetNeutralButton("انصراف", (s, ex) => { });
                    }
                    builder.Create().Show();
                }
                return false;

            }
            return true;
        }

        internal void RemoveFromCart(OrderItemModel item, Action callback = null)
        {
            if (CheckLogin())
            {
                var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
                builder.SetMessage("این گزینه از سبد خرید حذف شود؟");
                builder.SetPositiveButton("بله", (s, ex) =>
                {
                    Facade.Cart.Remove(item);
                    InvalidateOptionsMenu();
                    if (callback != null)
                        callback();
                });
                builder.SetNegativeButton("خیر", (s, ex) => { });
                builder.Create().Show();
            }
        }

        public void ShowCart()
        {
            if (CheckLogin())
            {
                //StartActivity(typeof(ShoppingCartActivity));
            }
        }

        public void ClearCart(Action action)
        {
            var builder = new Android.Support.V7.App.AlertDialog.Builder(this);
            builder.SetMessage("سبد خرید خالی شود؟");
            builder.SetPositiveButton("بله", (s, ex) =>
            {
                Facade.Cart.Clear();
                InvalidateOptionsMenu();
                if (action != null)
                    action();
            });
            builder.SetNegativeButton("خیر", (s, ex) => { });
            builder.Create().Show();
        }


        internal void ShowLoading(Action action)
        {
            var progressDialog = new ProgressDialog(this, ProgressDialog.ThemeDeviceDefaultLight);
            progressDialog.SetMessage("لطفا کمی صبر کنید...");
            RunOnUiThread(() => progressDialog.Show());
            new Thread(new ThreadStart(delegate
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    HandleException(e);
                }
                finally
                {
                    RunOnUiThread(() => progressDialog.Hide());
                }
            })).Start();
        }

        internal ProgressDialog ShowLoading()
        {
            var progressDialog = new ProgressDialog(this, ProgressDialog.ThemeDeviceDefaultLight);
            progressDialog.SetMessage("لطفا کمی صبر کنید...");
            RunOnUiThread(() => progressDialog.Show());
            return progressDialog;

        }

        internal void ShowPaymentSucceed()
        {
            RunOnUiThread(() => { Toast.MakeText(Application.Context, "عملیات پرداخت با موفقیت انجام شد.", ToastLength.Long).Show(); });
        }

        internal void ShowPaymentFailed()
        {
            RunOnUiThread(() => { Toast.MakeText(Application.Context, "عملیات پرداخت انجام نشد!", ToastLength.Long).Show(); });
        }


        internal void ShowPayment(OrderItemModel model)
        {
            if (CheckLogin() && CheckProfile())
            {
                LayoutInflater inflater = LayoutInflater;
                var builder = new Android.App.AlertDialog.Builder(this);
                View view = inflater.Inflate(Resource.Layout.layout_payment, null);
                builder.SetView(view);
                Android.App.AlertDialog dialog = builder.Create();
                view.FindViewById<TextView>(Resource.Id.tbxCredit).Text = String.Format("{0:#,###} ريال", Facade.Client.Credit);
                view.FindViewById<TextView>(Resource.Id.tbxTitle).Text = model.Title;
                view.FindViewById<TextView>(Resource.Id.tbxPrice).Text = String.Format("{0:#,###} ريال", model.Price);
                view.FindViewById<View>(Resource.Id.btnCredit).Click += delegate
                {
                    dialog.Hide();
                    ShowLoading(() =>
                    {
                        Facade.Cart.Clear();
                        Facade.Cart.Add(model);
                        var orderID = Facade.CheckCartCredit();
                        if (orderID > 0)
                        {
                            ShowPaymentSucceed();
                            SupportFragmentManager.PopBackStack();
                            Intent intent = new Intent(this, typeof(OrderDetailActivity));
                            intent.PutExtra("ID", orderID);
                            StartActivity(intent);
                        }
                        else
                            ShowPaymentFailed();
                    });

                };
                view.FindViewById<View>(Resource.Id.btnBank).Click += delegate
                {
                    dialog.Hide();
                    ShowLoading(() =>
                    {
                        Facade.Cart.Clear();
                        Facade.Cart.Add(model);
                        var result = Facade.CheckCart();
                        var intent = new Intent(this, typeof(BankGatewayActivity));
                        intent.PutExtra("PaymentID", result.PaymentID);
                        intent.PutExtra("PaymentUrl", result.PaymentUrl);
                        StartActivityForResult(intent, 5);
                    });
                };
                try
                {
                    if (!IsFinishing)
                    {
                        dialog.Show();
                    }

                }
                catch (Exception ex)
                {
                    HandleException(ex);
                }
            }
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 5)
            {
                if (resultCode == Android.App.Result.Ok)
                {
                    ShowPaymentSucceed();
                    var id = data.GetLongExtra("ID", 0);
                    Intent intent = new Intent(this, typeof(OrderDetailActivity));
                    intent.PutExtra("ID", id);
                    StartActivity(intent);
                }
                else
                {
                    ShowPaymentFailed();
                }
            }
        }

        #endregion
    }
}