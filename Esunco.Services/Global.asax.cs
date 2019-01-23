using AcoreX.Diagnostics;
using Esunco.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Esunco.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Mapper.Configure();
            Setup.StartTasks();
            Logger.SetResolver(new DefaultFileLogger());
        }

        void Application_Error(object sender, EventArgs e)
        {

            Logger.WriteLog(Server.GetLastError());
        }
    }
}
