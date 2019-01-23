using Esunco.Logics;
using Esunco.Logics.Contexts;
using Esunco.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Esunco.Services.Controllers
{
    public class AccountController : ApiController
    {
        [HttpPost]
        [Route("Account/SendSMS")]
        public JsonResult SendLoginSMS([FromBody]JObject model)
        {
            using (var ctx = new AccountContext())
            {
                ctx.SendLoginSMS(model.Value<string>("Mobile"));
                return new JsonResult();
            }
        }

        [HttpPost]
        [Route("Account/GetToken")]
        public JsonResult<string> GetToken([FromBody]JObject model)
        {
            using (var ctx = new AccountContext())
            {
                var mobile = model.Value<string>("Mobile");
                var code = model.Value<int>("Code");
                return new JsonResult<string>(ctx.GetRegisterationToken(mobile, code));
            }
        }

        [HttpPost]
        [Route("Account/SignIn")]
        public JsonResult<ClientProfileServiceModel> SignIn([FromBody]JObject model)
        {
            using (var ctx = new AccountContext())
            {
                var token = model.Value<string>("Token");
                return new JsonResult<ClientProfileServiceModel>(ctx.GetClientByToken(token));
            }
        }


        [HttpPost]
        [Route("Account/Register")]
        public JsonResult Register([FromBody]ClientProfileServiceModel model)
        {
            using (var ctx = new AccountContext())
            {
                ctx.Register(model);
                return new JsonResult();
            }
        }

        [HttpPost]
        [Route("Account/Save")]
        public JsonResult<ClientProfileServiceModel> SaveProfile([FromBody]ClientProfileServiceModel model)
        {
            using (var ctx = new AccountContext())
            {
                ctx.SaveProfile(model);
                return new JsonResult<ClientProfileServiceModel>(ctx.GetClientByToken(model.Token));
            }
        }

        [HttpGet]
        [Route("App/Info")]
        public JsonResult<AppInfoModel> GetAppInfo()
        {
            return new JsonResult<AppInfoModel>(new AppInfoModel {
                SupportPhone1 = Settings.SUPPORT_PHONE1,
                SupportPhone2 = Settings.SUPPORT_PHONE2,
                Version = Settings.APP_APP_VERSION,
                Message = "لطفا برنامه را بروز رسانی کنید",
            });
        }
    }
}
