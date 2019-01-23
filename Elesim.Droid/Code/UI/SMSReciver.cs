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
using Android.Telephony;
using Android.Provider;
using System.Text.RegularExpressions;
using AcoreX.Helper;

namespace Elesim.Droid.Code.UI
{
    [BroadcastReceiver(Enabled = true)]
    //[IntentFilter(new string[] { "android.provider.Telephony.SMS_RECEIVED" }, Priority = (int)IntentFilterPriority.LowPriority)]
    public class SMSReceiver : Android.Content.BroadcastReceiver
    {
        public static readonly string IntentAction = "android.provider.Telephony.SMS_RECEIVED";

        public override void OnReceive(Context context, Intent intent)
        {

            try
            {
                if (intent.Action != IntentAction) return;

                SmsMessage[] messages = Telephony.Sms.Intents.GetMessagesFromIntent(intent);

                var sb = new StringBuilder();

                for (var i = 0; i < messages.Length; i++)
                {
                    if (messages[i].OriginatingAddress.Contains("3000150150"))
                    {
                        MatchCollection regexPhone = Regex.Matches(messages[i].MessageBody, "\\d{4}", RegexOptions.RightToLeft | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
                        if (regexPhone.Count > 0)
                        {

                            int code = regexPhone[0].Value.ConvertTo<int>(0);
                            var intr = (IReceiveSMS)context;
                            intr.OnReceive(code);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(context, ex.Message, ToastLength.Long).Show();
            }
        }


    }

    public interface IReceiveSMS
    {
        void OnReceive(int code);
    }
}