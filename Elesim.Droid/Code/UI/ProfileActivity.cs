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
using Android.Support.Design.Widget;

namespace Elesim.Droid.Code.UI
{
    [Activity(Label = "ProfileActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class ProfileActivity : BaseActivity
    {
        EditText tbxFirstName;
        EditText tbxLastName;
        EditText tbxNationalCode;
        EditText tbxPhone;
        EditText tbxEmail;
        EditText tbxAddress;
        EditText tbxPostalCode;
        EditText tbxAccountType;
        TextInputLayout lytEmail;

        ClientProfileServiceModel clinetClone;

        Android.Support.V7.Widget.Toolbar toolbar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_profile);
            //Toolbar
            toolbar = (Android.Support.V7.Widget.Toolbar)FindViewById(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = Facade.Client.Fullname;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);

            tbxFirstName= FindViewById<EditText>(Resource.Id.tbxFirstName);
            tbxLastName= FindViewById<EditText>(Resource.Id.tbxLastName);
            tbxNationalCode = FindViewById<EditText>(Resource.Id.tbxNationalCode);
            tbxPhone = FindViewById<EditText>(Resource.Id.tbxPhone);
            tbxEmail = FindViewById<EditText>(Resource.Id.tbxEmail);
            tbxPostalCode = FindViewById<EditText>(Resource.Id.tbxPostalCode);
            tbxAddress = FindViewById<EditText>(Resource.Id.tbxAddress);
            tbxAccountType = FindViewById<EditText>(Resource.Id.tbxAccountType);
            lytEmail = FindViewById<TextInputLayout>(Resource.Id.input_layout_email);
            //
            clinetClone = Facade.Client.Clone<ClientProfileServiceModel>();
            //
            tbxFirstName.Text = clinetClone.Firstname;
            tbxLastName.Text = clinetClone.Lastname;
            tbxNationalCode.Text = clinetClone.NationalCode;
            tbxPhone.Text = clinetClone.Phone;
            tbxEmail.Text = clinetClone.Email;
            tbxAddress.Text = clinetClone.Address;
            tbxPostalCode.Text = clinetClone.PostalCode;
            tbxAccountType.Text = EnumHelper.GetEnumDescription(clinetClone.AccountType);
        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.profile, menu);
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
                case Resource.Id.action_save:
                    if (!Android.Util.Patterns.EmailAddress.Matcher(tbxEmail.Text).Matches())
                    {
                        lytEmail.Error = "ایمیل وارد شده صحیح نمی باشد.";
                        return false;
                    }
                    clinetClone.Firstname = tbxFirstName.Text;
                    clinetClone.Lastname = tbxLastName.Text;
                    clinetClone.NationalCode = tbxNationalCode.Text;
                    clinetClone.Phone = tbxPhone.Text;
                    clinetClone.Email = tbxEmail.Text;
                    clinetClone.Address = tbxAddress.Text;
                    clinetClone.PostalCode = tbxPostalCode.Text;
                    ShowLoading(delegate ()
                    {
                        Facade.SaveProfile(clinetClone);
                        Finish();
                    });
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}