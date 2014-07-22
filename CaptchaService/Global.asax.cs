using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApiContrib.Formatting.Jsonp;

namespace CaptchaService
{

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration.Formatters);

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(RouteTable.Routes);


            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //GlobalConfiguration.Configuration.Formatters.Insert(0, new TextMediaTypeFormatter());
            //GlobalConfiguration.Configuration.Formatters.AddJsonpFormatter();
        }
    }


    public class FormatterConfig
    {
        public static void RegisterFormatters(MediaTypeFormatterCollection formatters)
        {
            // Insert the JSONP formatter in front of the standard JSON formatter.
            var jsonpFormatter = new JsonpMediaTypeFormatter(formatters.JsonFormatter);
            formatters.Insert(0, jsonpFormatter);
        }
    }
}

//    public class TextMediaTypeFormatter : MediaTypeFormatter
//    {
//        public TextMediaTypeFormatter()
//        {
//            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
//        }

//        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
//        {
//            var taskCompletionSource = new TaskCompletionSource<object>();
//            try
//            {
//                var memoryStream = new MemoryStream();
//                readStream.CopyTo(memoryStream);
//                var s = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
//                taskCompletionSource.SetResult(s);
//            }
//            catch (Exception e)
//            {
//                taskCompletionSource.SetException(e);
//            }
//            return taskCompletionSource.Task;
//        }

//        public override bool CanReadType(Type type)
//        {
//            return type == typeof(string);
//        }

//        public override bool CanWriteType(Type type)
//        {
//            return false;
//        }
//    }
//}