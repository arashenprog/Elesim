using Android.Runtime;
using Android.Support.V4.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcoreX.Xamarin.Droid;

namespace Elesim.Droid.Code.Adapters
{
    public class ViewPagerAdapter : FragmentPagerAdapter
    {
        private List<Fragment> mFragmentList = new List<Fragment>();
        private List<String> mFragmentTitleList = new List<String>();

        public ViewPagerAdapter(FragmentManager manager)
            : base(manager)
        {

        }

        public override Fragment GetItem(int position)
        {
            return mFragmentList[position];
        }


        public override int Count
        {
            get { return mFragmentList.Count; }
        }


        public void AddFragment(Fragment fragment, String title)
        {
            mFragmentList.Add(fragment);
            mFragmentTitleList.Add(title);
        }



        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            return mFragmentTitleList[position].ToCharSequence();
        }



    }
}
