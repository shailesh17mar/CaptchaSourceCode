using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using WebApiContrib.Formatting.Jsonp;

namespace Test
{
    public class WebApiApplication : HttpApplication
    {

        protected void Application_Start()
        {
            FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration.Formatters);

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(RouteTable.Routes);

        }
    }

    public class FormatterConfig
    {
        public static void RegisterFormatters(MediaTypeFormatterCollection formatters)
        {
            var jsonpFormatter = new JsonpMediaTypeFormatter(formatters.JsonFormatter);
            formatters.Insert(0, jsonpFormatter);
        }
    }

    public static class WebApiConfig
    {
        public static void Register(RouteCollection routes)
        {
            var route = routes.MapHttpRoute(
                name: "CaptchaApi",
                routeTemplate: "api/Tavisca/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}