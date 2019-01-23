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

namespace Elesim.Droid.Code.UI
{
    public class HomeFragment : Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = false;
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var rootView = inflater.Inflate(Resource.Layout.fragment_home, container, false);
            rootView.FindViewById(Resource.Id.btnReqular).Click += delegate
            {
                Activity.StartActivity(typeof(RegularSimActivity));
            };
            rootView.FindViewById(Resource.Id.btnRond).Click += delegate
            {
                Activity.StartActivity(typeof(RondSimActivity));
            };
            rootView.FindViewById(Resource.Id.btnPackage).Click += delegate
            {
                Activity.StartActivity(typeof(PackListActivity));
            };
            rootView.FindViewById(Resource.Id.btnAuction).Click += delegate
            {
                Activity.StartActivity(typeof(AuctionListActivity));
            };

            return rootView;
        }


    }
}