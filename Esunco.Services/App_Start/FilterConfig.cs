﻿using Esunco.Services.Helpers;
using System.Web;
using System.Web.Mvc;

namespace Esunco.Services
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
