using Esunco.Logics.Contexts;
using Esunco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Esunco.Services.Controllers
{
    public class ReceiptsController : Controller
    {


        [Route("~/About")]
        public ActionResult About()
        {
            return View();
        }

        // GET: Receipts
        [HttpPost]
        [Route("~/Order/Receipt")]
        public ActionResult Index(string token, long orderId)
        {
            using (var ctx = new ServiceContext())
            {
                ReceiptModel model = ctx.GetOrderReceipt(token, orderId);
                if (model.Type == Models.Enum.OrderItemType.Sim)
                {
                    if (model.SimType == Models.Enum.SimType.PostPaid)
                        return View("PostPaid", model);
                    else
                        return View("PrePaid", model);
                }
                else if (model.Type == Models.Enum.OrderItemType.Pack)
                {
                    return View("Pack", model);
                }
                else
                {
                    return View("Auction", model);
                }
            }

        }

        [HttpGet]
        [Route("~/Order/Receipt")]
        public ActionResult IndexGet(string token, long orderId)
        {
            return Index(token, orderId);

        }

        [HttpGet]
        [Route("~/Order/Pack/Download/Zip/{id}")]
        public ActionResult DownloadZip(string id)
        {
            using (var ctx = new ServiceContext())
            {
                var bytes = ctx.GetOrderProtectedZipFile(id);

                return File(bytes, "application/zip", "elesim.zip");
            }

        }

        [HttpGet]
        [Route("~/Order/Pack/Download/Excel/{id}")]
        public ActionResult DownloadExcel(string id)
        {
            using (var ctx = new ServiceContext())
            {
                var bytes = ctx.GetOrderExcelFile(id);

                return File(bytes, "application/vnd.ms-excel", "elesim.xls");
            }

        }
    }
}