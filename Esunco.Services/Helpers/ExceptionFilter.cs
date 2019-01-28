using AcoreX.Diagnostics;
using Esunco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Esunco.Services.Helpers
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            Logger.WriteLog(context.Exception);
            var message = context.Exception.Message;
            var handled = context.Exception is AcoreX.Utility.HandledException || context.Exception is AcoreX.Utility.ExceptionHandler.BaseException;
            if (!handled)
            {
                message = "خطای ناشناخته";
            }

            context.Response = context.Request.CreateResponse<JsonResult>(HttpStatusCode.OK, new JsonResult
            {
                Messages = new List<string> { context.Exception.Message },
                Error = new JsonResultError { Message = message, Handled = handled },
                Succeed = false

            });
        }
    }
}