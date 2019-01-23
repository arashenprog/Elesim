using Esunco.Logics.Contexts;
using Esunco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Esunco.Services.Controllers
{
    public class BankController : Controller
    {
        [Route("Payment/Account/Charge")]
        [HttpPost]
        public ActionResult Charge(PaymentCreditModel model)
        {
            using (var ctx = new ServiceContext())
            {
                var result = ctx.RegisterCreditPayment(model);
                return View("Index", result);
            }
        }

        [HttpGet]
        [Route("Payment/Order")]
        public ActionResult Order(PaymentOrderModel model)
        {
            using (var ctx = new ServiceContext())
            {
                var result = ctx.RegisterOrderPayment(model);
                return View("Index", result);
            }
        }

        [HttpGet]
        [Route("Payment/Account/Charge/Callback")]
        public ActionResult AccountPaymentCallback(long id, string authority, string status)
        {
            using (var ctx = new ServiceContext())
            {
                var model = new PaymentCallbackModel { SaleOrderId = id, ResCode = status, RefId = authority };
                try
                {

                    ctx.VerifyCreditPayment(model);
                    ViewBag.Result = true;
                    ViewBag.Message = "پرداخت با موفقیت انجام شد";
                    return View("Callback", model);
                }
                catch (Exception ex)
                {
                    ViewBag.Result = false;
                    ViewBag.Message = ex.Message;
                    return View("Callback", model);
                }
            }
        }

        [HttpPost]
        [Route("Payment/Order/Callback")]
        public ActionResult PaymentOrderCallback(PaymentCallbackModel model)
        {
            using (var ctx = new ServiceContext())
            {
                try
                {
                    ctx.VerifyOrderPayment(model);
                    ViewBag.Result = true;
                    ViewBag.Message = "پرداخت با موفقیت انجام شد";
                    return View("Callback", model);
                }
                catch (Exception ex)
                {
                    ViewBag.Result = false;
                    ViewBag.Message = ex.Message;
                    return View("Callback", model);
                }
            }
        }


    }
}