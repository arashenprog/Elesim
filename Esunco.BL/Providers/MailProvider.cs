using AcoreX.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Esunco.Logics
{
    public class MailProvider : AcoreX.Utility.MailProvider
    {

        private System.Net.Mail.SmtpClient _client;

        public override System.Net.Mail.SmtpClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new SmtpClient(Settings.SMTP_SERVER, Settings.SMTP_PORT);
                    _client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    _client.EnableSsl = Settings.SMTP_SSL;
                    _client.UseDefaultCredentials = false;
                    _client.Timeout = 10000;
                }
                return _client;
            }
        }

        public override MailAccount DefaultSender
        {
            get
            {
                return new MailAccount(Settings.INFO_ADDRESS, Settings.INFO_USERNAME, Settings.INFO_PASSWORD, Settings.INFO_DISPLAYNAME);
            }
        }

        public override void OnException(MailExceptionEventArgs e)
        {
            //throw e.Exception;
        }
    }
}