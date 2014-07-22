using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.WebHost;
using System.Web.Routing;
using System.Web.SessionState;

namespace CaptchaService
{
    public static class WebApiConfig
    {
        public static void Register(RouteCollection routes)
        {
            var route = routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/Tavisca/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            route.RouteHandler = new CustomHttpControllerRouteHandler();
        }


    }

    public class CustomHttpControllerHandler    : HttpControllerHandler, IRequiresSessionState
        {
            public CustomHttpControllerHandler(RouteData routeData)  : base(routeData)
            { }
        }
        public class CustomHttpControllerRouteHandler : HttpControllerRouteHandler
        {
            protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
            {
                return new CustomHttpControllerHandler(requestContext.RouteData);
            }
        }

   
}
