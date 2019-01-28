using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.App;
using Android.Support.V4.View;
using Android.Graphics;
using Elesim.Droid.Code.Adapters;
using AcoreX.Xamarin.Droid;
using System.Threading;
using Elesim.Shared;
using System;
using Android.Graphics.Drawables;
using AcoreX.Helper;
using Android.Content;
using Android.Runtime;
using System.Web;

namespace Elesim.Droid.Code.UI
{
    [Activity(Label = "NavigationDrawer", Theme = "@style/AppTheme.NoActionBar")]
    [IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = "elesim")]
    public class MainActivity : BaseActivity
    {

        DrawerLayout drawer;
        Android.Support.V7.Widget.Toolbar toolbar;
        NavigationView navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            toolbar.SetLogo(Resource.Drawable.ic_logo_text);
            SupportActionBar.SetDisplayShowTitleEnabled(false);


            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.SetDrawerListener(toggle);
            toggle.SyncState();



            navigationView = (NavigationView)FindViewById(Resource.Id.nav_view);
            navigationView.NavigationItemSelected += NavigationView_NavigationItemSelected;
            View header = navigationView.GetHeaderView(0);
            header.Click += Header_Click;

            ShowHomeFragment();
            //
            Updater.Check(this, false);
        }

        private void ShowHomeFragment()
        {
            var ft = this.SupportFragmentManager.BeginTransaction();
            ft.Replace(Resource.Id.content, new HomeFragment());
            ft.Commit();
        }

        private void DetectUser()
        {
            View header = navigationView.GetHeaderView(0);
            new Thread(new ThreadStart(delegate
            {
                var token = AppPrefs.GetValue<string>("Token", null);
                if (token != null)
                {
                    try
                    {
                        Facade.SignIn(token);
                        RunOnUiThread(() =>
                        {
                            header.FindViewById<TextView>(Resource.Id.tbxClientName).Text = Facade.Client.Fullname;
                            header.FindViewById<TextView>(Resource.Id.tbxCredit).Text = String.Format("اعتبار فعلی شما: {0:#,###} ريال", Facade.Client.Credit);
                        });
                    }
                    catch (Exception ex)
                    {
                        RunOnUiThread(() =>
                        {
                            header.FindViewById<TextView>(Resource.Id.tbxClientName).Text = "لطفا به حساب کاربری خود وارد شوید";
                            header.FindViewById<TextView>(Resource.Id.tbxCredit).Text = "اگر حساب کاربری ندارید ثبت نام کنید";
                        });
                        //HandleException(ex);
                    }
                }
                else
                {
                    RunOnUiThread(() =>
                    {
                        header.FindViewById<TextView>(Resource.Id.tbxClientName).Text = "لطفا به حساب کاربری خود وارد شوید";
                        header.FindViewById<TextView>(Resource.Id.tbxCredit).Text = "اگر حساب کاربری ندارید ثبت نام کنید";
                    });
                }
            })).Start();
        }

        private void Header_Click(object sender, EventArgs e)
        {
            if (Facade.Client == null)
            {
                StartActivity(typeof(LoginActivity));
                //Finish();
            }
            else
            {
                StartActivity(typeof(ProfileActivity));
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            InvalidateOptionsMenu();
            DetectUser();
        }
        void NavigationView_NavigationItemSelected(object sender, NavigationView.NavigationItemSelectedEventArgs e)
        {
            // Close drawer
            drawer.CloseDrawer(GravityCompat.Start);
            switch (e.MenuItem.ItemId)
            {
                case Resource.Id.action_signout:
                    AppPrefs.Remove("Token");
                    Facade.SignOut();
                    DetectUser();
                    break;
                case Resource.Id.action_credit:
                    AddCredit();
                    break;
                case Resource.Id.action_about:
                    StartActivity(typeof(AboutActivity));
                    break;
                case Resource.Id.history:
                    if (base.CheckLogin())
                    {
                        StartActivity(typeof(OrderHistoryActivity));
                    }
                    break;
                case Resource.Id.action_account:
                    if (base.CheckLogin())
                    {
                        StartActivity(typeof(ProfileActivity));
                    }
                    break;
                case Resource.Id.action_update:
                    Updater.Check(this);
                    break;
            }
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main, menu);
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
                case Resource.Id.action_search:
                    StartActivity(typeof(SearchSimActivity));
                    break;

            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = (DrawerLayout)FindViewById(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        private void AddCredit()
        {
            if (CheckLogin())
            {
                LayoutInflater inflater = this.LayoutInflater;
                var builder = new Android.App.AlertDialog.Builder(this, Resource.Style.AppTheme_Alert);
                View view = inflater.Inflate(Resource.Layout.layout_credit, null);
                builder.SetView(view);
                Android.App.AlertDialog dialog = builder.Create();
                var tbxPrice = view.FindViewById<EditText>(Resource.Id.tbxPrice);
                tbxPrice.AfterTextChanged += TbxPrice_AfterTextChanged;
                view.FindViewById<View>(Resource.Id.btnBank).Click += delegate
                {
                    dialog.Hide();
                    ShowLoading(delegate
                    {
                        var id = view.FindViewById<RadioGroup>(Resource.Id.rbgList).CheckedRadioButtonId;
                        var rb = view.FindViewById<RadioButton>(id);

                        var amount = rb.Hint.DefaultIfNull<long>(0);
                        if (!String.IsNullOrWhiteSpace(tbxPrice.Text))
                        {
                            amount = tbxPrice.Text.Replace(",", "").DefaultIfNull<long>(0);
                            if (amount < 10000 || amount > 30000000)
                            {
                                throw new Exception("مبلغ وارد شده کمتر یا بیشتر از حد مجاز است.");
                            }
                        }
                        var result = Facade.ChargeAccount(amount);

                        var html = String.Format(@"<html>
                                        <body onLoad=""document.getElementById('form').submit()"">
                                            <form id=""form"" target =""_self"" method =""POST"" action = ""{0}"" >
                                               <input type =""hidden"" name=""mobile"" value=""{1}"" />
                                               <input type =""hidden"" name=""price"" value=""{2}"" />
                                            </form>
                                      </body>
                                    </html >", Facade.BaseUrl, Facade.Client.Mobile, amount);

                        String dataUri = "data:text/html," + System.Net.WebUtility.UrlEncode(html).Replace("\\+", "%20");
                        Intent intent = new Intent(Intent.ActionView, Android.Net.Uri.Parse(dataUri));
                        intent.AddFlags(ActivityFlags.NewTask);
                        StartActivity(intent);


                    });
                };
                dialog.Show();
            }
        }

        private void TbxPrice_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            var editText = sender as EditText;
            editText.AfterTextChanged -= TbxPrice_AfterTextChanged;
            var text = e.Editable.ToString();
            int value = 0;
            int.TryParse(text.Replace(",", ""), out value);
            editText.Text = string.Format("{0:#,###}", value);
            editText.SetSelection(editText.Text.Length);
            editText.AfterTextChanged += TbxPrice_AfterTextChanged;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 10)
            {
                if (resultCode == Android.App.Result.Ok)
                {
                    ShowPaymentSucceed();
                    DetectUser();
                }
                else
                {
                    ShowPaymentFailed();
                }
            }
        }
    }
}


