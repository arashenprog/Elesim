using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcoreX.Helper;

namespace Esunco.Logics
{
    public static class Settings
    {
        public static readonly long PAYMENT_TERMINAL_ID;
        public static readonly string PAYMENT_USERNAME;
        public static readonly string PAYMENT_PASSWORD;
        public static readonly string PAYMENT_BANK_URL;
        public static readonly string PAYMENT_ORDER_CALLBACK;
        public static readonly string PAYMENT_ACCOUNT_CALLBACK;
        public static readonly string PAYMENT_OREDR_DOWNLOAD_EXCEL;
        public static readonly string PAYMENT_OREDR_DOWNLOAD_ZIP;


        public static readonly string SMS_USERNAME;
        public static readonly string SMS_PASSWORD;
        public static readonly string SMS_NUMBER;


        public static readonly string SMTP_SERVER;
        public static readonly int SMTP_PORT;
        public static readonly bool SMTP_SSL;
        public static readonly string INFO_ADDRESS;
        public static readonly string INFO_USERNAME;
        public static readonly string INFO_PASSWORD;
        public static readonly string INFO_DISPLAYNAME;

        public static readonly int APP_APP_VERSION;

        public static readonly bool DEBUG_MODE;


        public static readonly string SUPPORT_PHONE1;
        public static readonly string SUPPORT_PHONE2;




        static Settings()
        {
            PAYMENT_TERMINAL_ID = ConfigurationManager.AppSettings["PAYMENT_TERMINAL_ID"].DefaultIfNull<long>(-1);
            PAYMENT_USERNAME = ConfigurationManager.AppSettings["PAYMENT_USERNAME"];
            PAYMENT_PASSWORD = ConfigurationManager.AppSettings["PAYMENT_PASSWORD"];
            PAYMENT_BANK_URL = ConfigurationManager.AppSettings["PAYMENT_BANK_URL"];
            PAYMENT_ORDER_CALLBACK = ConfigurationManager.AppSettings["PAYMENT_ORDER_CALLBACK"];
            PAYMENT_ACCOUNT_CALLBACK = ConfigurationManager.AppSettings["PAYMENT_ACCOUNT_CALLBACK"];
            PAYMENT_OREDR_DOWNLOAD_EXCEL = ConfigurationManager.AppSettings["PAYMENT_OREDR_DOWNLOAD_EXCEL"];
            PAYMENT_OREDR_DOWNLOAD_ZIP = ConfigurationManager.AppSettings["PAYMENT_OREDR_DOWNLOAD_ZIP"];
            //
            SMS_USERNAME = ConfigurationManager.AppSettings["SMS_USERNAME"];
            SMS_PASSWORD = ConfigurationManager.AppSettings["SMS_PASSWORD"];
            SMS_NUMBER = ConfigurationManager.AppSettings["SMS_NUMBER"];
            //
            SMTP_SERVER = ConfigurationManager.AppSettings["SMTP_SERVER"];
            SMTP_PORT = ConfigurationManager.AppSettings["SMTP_PORT"].DefaultIfNull<int>(0);
            SMTP_SSL = ConfigurationManager.AppSettings["SMTP_SSL"].DefaultIfNull<bool>(false);
            INFO_ADDRESS = ConfigurationManager.AppSettings["INFO_ADDRESS"];
            INFO_USERNAME = ConfigurationManager.AppSettings["INFO_USERNAME"];
            INFO_PASSWORD = ConfigurationManager.AppSettings["INFO_PASSWORD"];
            INFO_DISPLAYNAME = ConfigurationManager.AppSettings["INFO_DISPLAYNAME"];
            ///
            APP_APP_VERSION = ConfigurationManager.AppSettings["APP_APP_VERSION"].DefaultIfNull<int>(0);
            //
            SUPPORT_PHONE1 = ConfigurationManager.AppSettings["SUPPORT_PHONE1"];
            SUPPORT_PHONE2 = ConfigurationManager.AppSettings["SUPPORT_PHONE2"];
            //
            DEBUG_MODE = ConfigurationManager.AppSettings["DEBUG_MODE"].DefaultIfNull<bool>(false);
        }
    }
}
