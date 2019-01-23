using AcoreX.Web;
using AcoreX.Web.Razor;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OHS.BL.Providers
{
    public class WebHtmlTemplateProvider : HtmlTemplateProvider
    {
        public override string Render<T>(string templateName, T model)
        {
            try
            {
                //var layout = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/Shared/Mails/_EmailLayout.cshtml"));
                //var content = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath(String.Format("~/Views/Shared/Mails/{0}.cshtml", templateName)));

                var layout = System.IO.File.ReadAllText(Settings.Mail.TemplateFolder + "\\_EmailLayout.cshtml");
                var content = System.IO.File.ReadAllText(String.Format("{0}\\{1}.cshtml", Settings.Mail.TemplateFolder, templateName));
                //

                var t = RazorEngine.Razor.GetTemplate(layout, "MailLayout");
                RazorEngine.Razor.Compile(layout, "MailLayout");
                var resultView = RazorEngine.Razor.Parse(content, model);
                return resultView;


                //IRazorTemplateGenerator generator = new RazorTemplateGenerator();

                //generator.RegisterTemplate<T>(content);
                //generator.CompileTemplates();
                //return generator.GenerateOutput(model);

            }
            catch (Exception e)
            {
                //IM.ExceptionHandler.Reporter.Report(e);
                throw e;
            }
        }
    }
}