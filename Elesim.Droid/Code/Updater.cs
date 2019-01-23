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
using System.Net;
using Elesim.Shared;
using System.Threading.Tasks;
using Android.Support.V4.App;

namespace Elesim.Droid.Code
{
    public static class Updater
    {
        private const int id = 1337;

        public static void Check(Activity context, bool showToast = true)
        {

            NotificationManager notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);

            Task.Factory.StartNew(() =>
            {
                var version = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionCode;

                var facade = new Facade();
                var info = facade.GetAppInfo();

                if (info.Version > version)
                {
                    var webClient = new WebClient();
                    var path = string.Format("{0}/download/elesim-{1}.apk", Android.OS.Environment.ExternalStorageDirectory, info.Version);
                    if (System.IO.File.Exists(path))
                    {
                        Install(context, path);
                        return;
                    }

                    webClient.DownloadFileAsync(new Uri("http://elesim.ir/download/elesim.apk"), path);

                    var builder = new NotificationCompat.Builder(context);
                    builder.SetContentTitle("بروز رسانی");
                    builder.SetContentText("در حال دریافت فایل...");
                    builder.SetSmallIcon(Resource.Drawable.ic_launcher);
                    notificationManager.Notify(id, builder.Build());


                    webClient.DownloadProgressChanged += (s, e) =>
                    {
                        int length = Convert.ToInt32(e.TotalBytesToReceive.ToString());
                        int prog = Convert.ToInt32(e.BytesReceived.ToString());
                        int perc = Convert.ToInt32(e.ProgressPercentage.ToString());
                        builder.SetProgress(100, perc, false);
                        //builder.SetContentText("Tap to Cancel Upload");
                        //builder.SetContentIntent(pending);
                        notificationManager.Notify(id, builder.Build());
                    };

                    webClient.DownloadFileCompleted += (s, e) =>
                    {
                        notificationManager.Cancel(id);
                        Install(context, path);
                    };
                }
                else
                {
                    if (showToast)
                        context.RunOnUiThread(() =>
                        {
                            Toast.MakeText(context, "نرم افزار شما بروز می باشد!", ToastLength.Long).Show();
                        });

                }
            });
        }

        private static void Install(Activity context, string path)
        {
            context.RunOnUiThread(() =>
            {
                var builder = new Android.Support.V7.App.AlertDialog.Builder(context);
                builder.SetMessage("نسخه جدید نرم افزار الِ سیم موجود است! بروزرسانی کنید.");
                builder.SetPositiveButton("بروزرسانی", (s, ex) =>
                {
                    Intent promptInstall = new Intent(Intent.ActionView).SetDataAndType(Android.Net.Uri.FromFile(new Java.IO.File(path)), "application/vnd.android.package-archive");
                    promptInstall.AddFlags(ActivityFlags.NewTask);
                    context.StartActivity(promptInstall);
                });
                builder.SetNegativeButton("انصراف", (s, ex) => { });
                builder.Create().Show();
            });
        }
    }
}