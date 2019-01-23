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
using Android.Graphics;
using Java.Lang.Reflect;
using Android.Support.V7.App;

namespace Elesim.Droid.Code
{
    [Application]
    public class MyApp : Application
    {
   

        public MyApp(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
        public override void OnCreate()
        {
            base.OnCreate();
            //app init ...
            AppCompatDelegate.CompatVectorFromResourcesEnabled = true;
            Typeface customFontTypeface = Typeface.CreateFromAsset(Context.Assets, "fonts/IRANSansMobile.ttf");
            try
            {
                Field serifFontTypefaceField = Java.Lang.Class.FromType(typeof(Typeface)).GetDeclaredField("SERIF");
                serifFontTypefaceField.Accessible = true;
                serifFontTypefaceField.Set(null, customFontTypeface);


                Field monoFontTypefaceField = Java.Lang.Class.FromType(typeof(Typeface)).GetDeclaredField("MONOSPACE");
                monoFontTypefaceField.Accessible = true;
                monoFontTypefaceField.Set(null, customFontTypeface);

                Field normalFontTypefaceField = Java.Lang.Class.FromType(typeof(Typeface)).GetDeclaredField("NORMAL");
                normalFontTypefaceField.Accessible = true;
                normalFontTypefaceField.Set(null, customFontTypeface);


            }
            catch (Exception e)
            {

            }
            //
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException; ;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            //Toast.MakeText(this.BaseContext, "خطا در اجرای برنامه!", ToastLength.Long).Show();
        }
    }
}