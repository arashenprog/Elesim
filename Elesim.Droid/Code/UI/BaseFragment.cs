
using Android.App;
using Android.Support.V4.App;
using Elesim.Shared;

namespace Elesim.Droid.Code.UI
{
    public abstract class BaseFragment : Android.Support.V4.App.Fragment
    {


        public new BaseActivity BaseActivity
        {
            get
            {
                return Activity as BaseActivity;
            }
        }

        public Facade Facade
        {

            get
            {
                return (Activity as BaseActivity).Facade;
            }
        }
    }
}