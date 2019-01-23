using Esunco.Logics.Contexts;
using Esunco.Models;
using Esunco.Models.Enum;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Esunco.Services.Controllers
{
    public class PaymentController : ApiController
    {

        [HttpPost]
        [Route("Order/Payment")]
        public JsonResult<PaymentResultModel> Send([FromBody]PaymentOrderModel model)
        {
            using (var ctx = new ServiceContext())
            {
                var result = ctx.RegisterOrderPayment(model);
                return new JsonResult<PaymentResultModel>(result);
            }
        }

        [HttpPost]
        [Route("Order/Payment/Credit")]
        public JsonResult<long> OrderWithCredit([FromBody]PaymentOrderModel model)
        {
            using (var ctx = new ServiceContext())
            {
                var result = ctx.RegisterOrderWithCredit(model);
                return new JsonResult<long>(result);
            }
        }

        [HttpPost]
        [Route("Account/Credit/Payment")]
        public JsonResult<PaymentResultModel> Send([FromBody]PaymentCreditModel model)
        {
            using (var ctx = new ServiceContext())
            {
                var result = ctx.RegisterCreditPayment(model);
                return new JsonResult<PaymentResultModel>(result);
            }
        }


        [HttpPost]
        [HttpGet]
        [Route("Order/Payment/Callback")]
        public JsonResult OrderPaymentCallback([FromBody]PaymentCallbackModel model)
        {
            using (var ctx = new ServiceContext())
            {
                ctx.VerifyOrderPayment(model);
                return new JsonResult(true);
            }
        }

        [HttpPost]
        [HttpGet]
        [Route("Account/Payment/Callback")]
        public JsonResult AccountPaymentCallback([FromBody]PaymentCallbackModel model)
        {
            using (var ctx = new ServiceContext())
            {
                ctx.VerifyCreditPayment(model);
                return new JsonResult(true);
            }
        }


        [HttpPost]
        [Route("Payment/Status")]
        public JsonResult<PaymentStatus> GetPaymentStatus([FromBody]JObject model)
        {
            using (var ctx = new ServiceContext())
            {
                var result = ctx.GetPaymentStatus(model.Value<string>("Token"), model.Value<string>("PaymentID"));
                return new JsonResult<PaymentStatus>(result);
            }
        }
    }
}
