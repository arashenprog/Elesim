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
using System.Timers;

namespace Elesim.Droid.Code.UI
{

    [Activity(Label = "سیم کارت های معمولی", Theme = "@style/AppTheme.NoActionBar")]
    public class RegularSimActivity : RondSimActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SupportActionBar.Title = "سیم کارت های معمولی";
        }

        protected override async Task<List<SimServiceModel>> GetList(long lastId)
        {
            return await Facade.GetReqularSims(lastId);
        }
    }
}